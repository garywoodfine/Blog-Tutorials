using Newtonsoft.Json;

namespace WorldBank.Models
{
    public class Country
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("iso2Code")]
        public string Iso2Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("region")]
        public AdminRegion Region { get; set; }

        [JsonProperty("adminregion")]
        public AdminRegion Adminregion { get; set; }

        [JsonProperty("incomeLevel")]
        public AdminRegion IncomeLevel { get; set; }

        [JsonProperty("lendingType")]
        public AdminRegion LendingType { get; set; }

        [JsonProperty("capitalCity")]
        public string CapitalCity { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }
    }
}