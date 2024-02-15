using Newtonsoft.Json;


namespace CryptoScanner.App.ApiModels
{
    public class CoinListRoot
    {
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("id")]
        public string? ApiId { get; set; }

    }
    public class CoinRoot
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("market_data")]
        public MarketData? MarketData { get; set; }
        [JsonProperty("image")]
        public Image? Image { get; set; }
    }
    public class Image
    {
        [JsonProperty("small")]
        public string? Thumbnail { get; set; }
    }
    public class MarketData
    {
        [JsonProperty("current_price")]
        public CurrentPrice CurrentPrice { get; set; }
        [JsonProperty("price_change_percentage_24h")]
        public decimal PriceChange24hPercent { get; set; }

    }

    public class CurrentPrice
    {
        [JsonProperty("sek")]
        public decimal Sek { get; set; }
        [JsonProperty("usd")]
        public decimal Dollar { get; set; }
        [JsonProperty("eur")]
        public decimal Euro { get; set; }
    }
}
