using System.Collections.Generic;
using Threenine.ApiResponse;

namespace Boleyn.Countries.Activities.Country.Get
{
    public class Response  : SingleResponse<CountryDetail>
    {
        public CountryDetail Item { get; }


        public Response(CountryDetail model,  IList<KeyValuePair<string, string[]>> validationErrors = null) : base(model, validationErrors)
        {
        }
    }

    public class CountryDetail
    {
        public string Name { get; set; }
        public string Region { get; set; }
        public string Capital { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }
}