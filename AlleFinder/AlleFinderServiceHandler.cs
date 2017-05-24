using System.Collections.Generic;
using System.Linq;
using AlleFinder.AlleFinderServiceReference;
using AlleFinder.AllegroServiceReference;
using Newtonsoft.Json;

namespace AlleFinder
{
    public class AlleFinderServiceHandler
    {
        private readonly AlleFinderServiceClient _client = new AlleFinderServiceClient();

        public CatInfoType[] GetCategoriesListByPhrase(string phrase)
        {
            string categoriesListJson = _client.GetCategoriesListByPhraseJson(phrase);
            return JsonConvert.DeserializeObject<CatInfoType[]>(categoriesListJson);
        }

        public FiltersListType[] GetFiltersListByCategoryIdJson(string categoryId)
        {
            string filtersListType = _client.GetFiltersListByCategoryIdJson(categoryId);
            return JsonConvert.DeserializeObject<FiltersListType[]>(filtersListType);
        }

        public string[] GetCategoriesListPathsByPhrase(string phrase)
            => _client.GetCategoriesListPathsByPhrase(phrase);

        public ItemsListType SetItemOnTop(string filtersJson)
        {
            string itemJson = _client.GetItemsListJson(filtersJson, 1, 0);
            return itemJson != "null" ? JsonConvert.DeserializeObject<ItemsListType[]>(itemJson)[0] : null;
        }

        public int CountNewerItemsThanSelected(string filtersJson, ItemsListType itemOnTop)
        {
            int count = 0;
            const int size = 50;
            for (int offset = 0; ; ++offset)
            {
                string itemsListJson = _client.GetItemsListJson(filtersJson, size, offset);
                if (itemsListJson == "null")
                    break;
                List<ItemsListType> itemsList = JsonConvert.DeserializeObject<ItemsListType[]>(itemsListJson).ToList();
                var foundItem = itemsList.FirstOrDefault(n => n.itemId == itemOnTop.itemId);
                if (foundItem != null)
                {
                    count += itemsList.IndexOf(foundItem);
                    break;
                }
                count += size;
            }
            return count;
        }
    }
}
