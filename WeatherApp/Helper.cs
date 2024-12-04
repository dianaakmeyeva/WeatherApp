using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Documents;
using WeatherApp.Model;

namespace WeatherApp
{
    internal class Helper
    {
        private static string ApiKey = "i74a6Lqkh7ZbXLJP2S4D79PvLAbU5YXU";
        private static string AutoComplect =
            "http://dataservice.accuweather.com/locations/v1/cities/autocomplete?apikey={0}&q={1}";
        private static string CurrentConditions =
            "http://dataservice.accuweather.com/currentconditions/v1/{0}?apikey={1}";

        public async static Task<List<City>> GetCities(string query)
        {
            List<City> list = new List<City>();

            string url = String.Format(AutoComplect, ApiKey, query);

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();
                list = JsonConvert.DeserializeObject<List<City>>(json);
            }

            return list;
        }

        public async static Task<CurrentConditions> GetCurrentAsync(string cityId)
        {
            CurrentConditions currentConditions = null;

            string url = String.Format(CurrentConditions, cityId, ApiKey);

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                currentConditions = JsonConvert.DeserializeObject<List<CurrentConditions>>(json).FirstOrDefault();
            }

            return currentConditions;
        }
    }
}
