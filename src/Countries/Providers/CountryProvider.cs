using System.Net.Http;
using System.Threading.Tasks;
using Boleyn.Countries.Content.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WorldBank.Models;

namespace Boleyn.Countries.Content.Providers
{
    public class CountryProvider : IProvider<WorldBank.Models.Country>
    {
        private readonly HttpClient _httpClient;

        public CountryProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Country> Get(string predicate)
        {
            var response = await _httpClient.GetAsync($"{predicate}?format=json");
            if (!response.IsSuccessStatusCode) return null;
            
            var responseJson = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(responseJson)) return null;

            if (responseJson.Contains("message")) throw new CountryNotFoundException($"No Country found for code {predicate}");
            var obj = JsonConvert.DeserializeObject<JArray>(responseJson);
            return  JsonConvert.DeserializeObject<Country>(obj[1][0].ToString());;

           

        }
    }
}