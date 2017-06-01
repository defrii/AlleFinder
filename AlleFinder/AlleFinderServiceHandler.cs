using System.Threading;
using System.Threading.Tasks;
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

        public async Task<CatInfoType[]> GetCategoriesListByPhraseAsync(string phrase)
        {
            string categoriesListJson = await _client.GetCategoriesListByPhraseJsonAsync(phrase);
            return JsonConvert.DeserializeObject<CatInfoType[]>(categoriesListJson);
        }

        public async Task<CatInfoType[]> GetCategoriesListByPhraseAsync(string phrase, CancellationToken cancellationToken)
        {
            string categoriesListJson = await _client.GetCategoriesListByPhraseJsonAsync(phrase);
            cancellationToken.ThrowIfCancellationRequested();
            return JsonConvert.DeserializeObject<CatInfoType[]>(categoriesListJson);
        }

        public FiltersListType[] GetFiltersListByCategoryIdJson(string categoryId)
        {
            string filtersListType = _client.GetFiltersListByCategoryIdJson(categoryId);
            return JsonConvert.DeserializeObject<FiltersListType[]>(filtersListType);
        }

        public async Task<FiltersListType[]> GetFiltersListByCategoryIdJsonAsync(string categoryId)
        {
            string filtersListType = await _client.GetFiltersListByCategoryIdJsonAsync(categoryId);
            return JsonConvert.DeserializeObject<FiltersListType[]>(filtersListType);
        }

        public async Task<FiltersListType[]> GetFiltersListByCategoryIdJsonAsync(string categoryId, CancellationToken cancellationToken)
        {
            string filtersListType = await _client.GetFiltersListByCategoryIdJsonAsync(categoryId);
            cancellationToken.ThrowIfCancellationRequested();
            return JsonConvert.DeserializeObject<FiltersListType[]>(filtersListType);
        }

        public string[] GetCategoriesListPathsByPhrase(string phrase)
            => _client.GetCategoriesListPathsByPhrase(phrase);

        public async Task<string[]> GetCategoriesListPathsByPhraseAsync(string phrase)
            => await _client.GetCategoriesListPathsByPhraseAsync(phrase);

        public async Task<string[]> GetCategoriesListPathsByPhraseAsync(string phrase, CancellationToken cancellationToken)
        {
            string[] paths = await _client.GetCategoriesListPathsByPhraseAsync(phrase);
            cancellationToken.ThrowIfCancellationRequested();
            return paths;
        }
    }
}
