using CryptoScanner.App.ApiModels;
using CryptoScanner.Data.Models;
using CryptoScanner.Data.Repositories.Interfaces;
using Newtonsoft.Json;

namespace CryptoScanner.App
{
    public class ApiCaller
    {
        private readonly ICoinsRepository repo;
        public HttpClient Client { get; set; }
        public ApiCaller(ICoinsRepository repo)
        {
            this.repo = repo;
            Client = new HttpClient();
            Client.BaseAddress = new Uri("https://api.coingecko.com/api/v3/");
        }

        public async Task<CoinModel> MakeCall(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name was null");
            }

            var dbTry = await repo.Get(name);
            if (dbTry != null)
            {
                return dbTry;
            }

            HttpResponseMessage response = await Client.GetAsync("coins/list");
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException((HttpRequestError)response.StatusCode);
            }

            string json = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<CoinListRoot>>(json);

            if (result != null)
            {
                CoinListRoot? searchedObject = result.FirstOrDefault(r => r.Name.ToLower() == name.ToLower());

                if (searchedObject != null && searchedObject.ApiId != null)
                {
                    var coinToAdd = await GetById(searchedObject.ApiId);
                    await repo.AddCoin(coinToAdd);
                    return coinToAdd;
                }
            }

            throw new Exception("There was some problems getting that currency");
        }

        public async Task<CoinModel> GetById(string apiId)
        {
            HttpResponseMessage response = await Client.GetAsync($"coins/{apiId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException((HttpRequestError)response.StatusCode);
            }

            string json = await response.Content.ReadAsStringAsync();

            if (json == null)
            {
                throw new JsonSerializationException();
            }
            var result = JsonConvert.DeserializeObject<CoinRoot>(json);

            CoinModel coin = new CoinModel()
            {
                Name = result.Name,
                ApiId = result.Id,
                CurrentPriceSek = result.MarketData.CurrentPrice.Sek,
                CurrentPriceDollar = result.MarketData.CurrentPrice.Dollar,
                CurrentPriceEuro = result.MarketData.CurrentPrice.Euro,
                PriceChange24hPercent = result.MarketData.PriceChange24hPercent,
                LastUpdated = DateTime.Now,
                Thumbnail = result.Image.Thumbnail,


            };

            return coin;

        }
    }
}
