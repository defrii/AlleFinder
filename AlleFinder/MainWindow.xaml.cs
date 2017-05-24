using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using AlleFinder.AllegroServiceReference;

namespace AlleFinder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _lastCategoryTest = "";
        private TextBox _categoryTextBox;
        private readonly AlleFinderServiceHandler _serviceHandler = new AlleFinderServiceHandler();
        private readonly List<FilterOptionsType> _filters = new List<FilterOptionsType>();
        private CatInfoType _currentCategory;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            _categoryTextBox = (TextBox)CategoryComboBox.Template.FindName("PART_EditableTextBox", CategoryComboBox);
            _categoryTextBox.TextChanged += (o, args) => ConfigureSuggestionsList();
            CategoryComboBox.SelectionChanged += OnCategorySelected;
        }

        private void OnCategorySelected(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            if (CategoryComboBox.SelectedItem == null) return;

            _currentCategory = DeepCopyOfCatInfoType((CatInfoType)CategoryComboBox.SelectedItem);

            List<FiltersListType> downloadedFilters = _serviceHandler.GetFiltersListByCategoryIdJson(_currentCategory.catId.ToString()).ToList();

            int count = 0;
            if (downloadedFilters.Count(n => n.filterName == "Stan") == 2)
            {
                var duplicatedItem = downloadedFilters.FindLast(n => n.filterName == "Stan");
                downloadedFilters.Remove(duplicatedItem);
            }
            foreach (var dFilter in downloadedFilters)
            {
                if (IsUselessFilter(dFilter)) continue;

                var filtersPanel = count % 2 == 0
                    ? (StackPanel)FiltersContainersGrid.Children[0]
                    : (StackPanel)FiltersContainersGrid.Children[1];

                StackPanel panel = new StackPanel();
                filtersPanel.Children.Add(panel);

                Label nameLabel = new Label
                {
                    Content = dFilter.filterName,
                    FontWeight = FontWeights.Bold
                };
                panel.Children.Add(nameLabel);

                //from-to value
                if (dFilter.filterIsRange)
                {
                    panel.Orientation = Orientation.Horizontal;
                    nameLabel.Content += " (od | do)";
                    FilterOptionsType inputFilter = new FilterOptionsType
                    {
                        filterId = dFilter.filterName,
                        filterValueRange = new RangeValueType()
                    };

                    Thickness margin = new Thickness(0, 0, 5, 0);
                    TextBox fromTextBox = new TextBox
                    {
                        Width = 50,
                        Margin = margin
                    };
                    Binding fromBinding = new Binding
                    {
                        Source = inputFilter.filterValueRange,
                        Path = new PropertyPath(nameof(inputFilter.filterValueRange.rangeValueMin))
                    };

                    fromTextBox.SetBinding(TextBox.TextProperty, fromBinding);
                    panel.Children.Add(fromTextBox);

                    TextBox toTextBox = new TextBox { Width = 50 };
                    Binding toBinding = new Binding
                    {
                        Source = inputFilter.filterValueRange,
                        Path = new PropertyPath(nameof(inputFilter.filterValueRange.rangeValueMax))
                    };
                    toTextBox.SetBinding(TextBox.TextProperty, toBinding);
                    panel.Children.Add(toTextBox);

                    _filters.Add(inputFilter);
                }
                //value to choose
                else if (dFilter.filterValues != null)
                {
                    panel.Orientation = Orientation.Vertical;

                    foreach (var value in dFilter.filterValues)
                    {
                        Thickness padding = new Thickness(0, 0, 0, 0);
                        Label valueLabel = new Label
                        {
                            Content = value.filterValueName,
                            Padding = padding,
                            Cursor = Cursors.Hand
                        };
                        panel.Children.Add(valueLabel);

                        FilterOptionsType inputFilter = new FilterOptionsType
                        {
                            filterId = dFilter.filterId,
                            filterValueId = new[] { value.filterValueName }
                        };

                        valueLabel.MouseLeftButtonUp +=
                            delegate
                            {
                                if (_filters.IndexOf(inputFilter) == -1)
                                {
                                    _filters.Add(inputFilter);
                                    valueLabel.FontWeight = FontWeights.DemiBold;
                                    Debug.WriteLine(inputFilter.filterId);
                                }
                                else
                                {
                                    _filters.Remove(inputFilter);
                                    valueLabel.FontWeight = FontWeights.Normal;
                                }
                            };
                    }
                }
                //single value input
                else
                {
                    panel.Orientation = Orientation.Horizontal;

                    FilterOptionsType inputFilter = new FilterOptionsType
                    {
                        filterId = dFilter.filterId,
                        filterValueId = new string[1]
                    };

                    TextBox inputTextBox = new TextBox { Width = 150 };
                    inputTextBox.TextChanged += (o, args) => inputFilter.filterValueId[0] = inputTextBox.Text;

                    _filters.Add(inputFilter);
                    panel.Children.Add(inputTextBox);
                }
                ++count;
            }
        }

        private void ConfigureSuggestionsList()
        {
            if (IsTextLongerThan(3) && IsTextLengthIncreased())
            {
                CatInfoType[] categoriesListNames = _serviceHandler.GetCategoriesListByPhrase(_categoryTextBox.Text);
                string[] categoriesListPaths = _serviceHandler.GetCategoriesListPathsByPhrase(_categoryTextBox.Text);

                for (int i = 0; i < categoriesListNames.Length; ++i)
                    categoriesListNames[i].catName = categoriesListPaths[i];

                CategoryComboBox.ItemsSource = categoriesListNames;
                CategoryComboBox.DisplayMemberPath = "catName";
                CategoryComboBox.IsDropDownOpen = true;
                _categoryTextBox.CaretIndex = _categoryTextBox.Text.Length;
            }
            else if (!IsTextLengthIncreased())
            {
                ResetFilters();
                _currentCategory = null;
                foreach (var filtersGrid in FiltersContainersGrid.Children)
                    ((StackPanel)filtersGrid).Children.Clear();
                CategoryComboBox.ItemsSource = null;
                CategoryComboBox.IsDropDownOpen = false;
            }
            _lastCategoryTest = _categoryTextBox.Text;
        }

        private bool IsTextLongerThan(int length) =>
            _categoryTextBox.Text.Length >= length;
        private bool IsTextLengthIncreased() =>
            _categoryTextBox.Text.Length > _lastCategoryTest.Length;


        private void RedirectToWebsite(object sender, RoutedEventArgs e)
        {
            string link = "https://allegro.pl/";
            string categoryPath = GetParsedCategoryName();
            if (categoryPath.Length > 0)
                link += categoryPath;
            else
                link += "listing?";
            if (PhraseTextBox.Text.Length > 0)
                link += "string=" + PhraseTextBox.Text + "&";
            foreach (var filter in _filters)
            {
                switch (filter.filterId)
                {
                    case "offerType":
                        switch (filter.filterValueId[0])
                        {
                            case "Kup Teraz":
                                link += "offerTypeBuyNow=1&";
                                break;
                            case "Licytacje":
                                link += "offerTypeAuction=2&";
                                break;
                        }
                        break;
                    case "condition":
                        switch (filter.filterValueId[0])
                        {
                            case "Nowe":
                                link += "buyNew=1&";
                                break;
                            case "Używane":
                                link += "buyUsed=1&";
                                break;
                        }
                        break;
                    case "Cena":
                        if (!string.IsNullOrEmpty(filter.filterValueRange.rangeValueMin))
                            link += "price_from=" + filter.filterValueRange.rangeValueMin + "&";
                        if (!string.IsNullOrEmpty(filter.filterValueRange.rangeValueMax))
                            link += "price_to=" + filter.filterValueRange.rangeValueMax + "&";
                        break;
                    case "offerOptions":
                        switch (filter.filterValueId[0])
                        {
                            case "Wysyłka gratis":
                                link += "freeShipping=1&";
                                break;
                            case "Allegro InPost":
                                link += "freeReturn=1&";
                                break;
                            case "odbiór w Paczkomacie":
                                link += "generalDelivery_rec=1&";
                                break;
                            case "Standard Allegro":
                                link += "standard_allegro=1&";
                                break;
                            case "Faktura VAT":
                                link += "vat_invoice=1&";
                                break;
                            case "Odbiór osobisty":
                                link += "personal_rec=1&";
                                break;
                        }
                        break;
                    default:
                        if (filter.filterValueRange?.rangeValueMin?.Length > 0)
                            link += ParseToParameter(filter.filterId) + "-od=" + filter.filterValueRange.rangeValueMin + "&";
                        if (filter.filterValueRange?.rangeValueMax?.Length > 0)
                            link += ParseToParameter(filter.filterId) + "-do=" + filter.filterValueRange.rangeValueMax + "&";
                        if (filter.filterValueId != null && filter.filterValueId[0]?.Length > 0)
                            link += filter.filterId + "=" + HttpUtility.UrlEncode(filter.filterValueId[0]) + "&";
                        break;
                }
            }

            if (link[link.Length - 1] == '&')
                link = link.Substring(0, link.Length - 1);
            Process.Start(link);
        }

        private string GetParsedCategoryName()
        {
            if (_currentCategory == null) return "";

            string name = "";
            string categoryName = _currentCategory.catName;
            for (int i = categoryName.Length - 1; i >= 0; --i)
            {
                if (categoryName[i] == '>')
                    break;
                name = categoryName[i] + name;
            }
            name = ParseToParameter(name) + "-" + _currentCategory.catId;
            name = "kategoria/" + name + "?";
            return name;
        }

        private string ParseToParameter(string str)
        {
            str = RemoveDiacritics(str.TrimStart(' ')).ToLower();
            str = Regex.Replace(str, @"[,.()]", "");
            str = Regex.Replace(str, @"\W", "-");
            return str;
        }

        private string RemoveDiacritics(string str)
        {
            StringBuilder builder = new StringBuilder(str);
            string diacritics = "ąćęłńóśźż";
            string normalized = "acelnoszz";
            for (int i = 0; i < str.Length; ++i)
            {
                for (int j = 0; j < diacritics.Length; ++j)
                {
                    if (builder[i] == diacritics[j])
                    {
                        builder[i] = normalized[j];
                        break;
                    }
                }
            }
            return builder.ToString();
        }

        private bool IsUselessFilter(FiltersListType filter)
        {
            string[] uselessFilters =
            {
                "userId", "search", "state", "distance", "postCode", "category", "department", "endingTime",
                "shippingTime", "special"
            };
            return uselessFilters.Contains(filter.filterId);
        }

        private void ResetFilters()
        {
            _filters.Clear();
            _filters.Add(new FilterOptionsType { filterId = "search", filterValueId = new[] { PhraseTextBox.Text } });
        }

        private CatInfoType DeepCopyOfCatInfoType(CatInfoType cat)
        {
            CatInfoType copy = new CatInfoType
            {
                catName = cat.catName,
                catId = cat.catId,
                catIsProductCatalogueEnabled = cat.catIsProductCatalogueEnabled,
                catParent = cat.catParent,
                catPosition = cat.catPosition
            };

            return copy;
        }
    }
}
