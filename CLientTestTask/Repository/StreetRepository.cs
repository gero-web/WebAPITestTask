using CLientTestTask.Models;
using CLientTestTask.Repository.Bases;
using CLientTestTask.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CLientTestTask.Repository
{
    
    public class StreetRepository : IRepository<Street>
    {

        private readonly HttpClient _httpClient;

        public StreetRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public Street CreateValue(Street value)
        {
            var data = JsonConvert.SerializeObject(value);

            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync($"{APIUrl.HOST_URL}api/Streets", content).Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<Street>(responseBody);
        }

        public string Delete(int id)
        {
            var response = _httpClient.DeleteAsync($"{APIUrl.HOST_URL}api/Streets/{id}").Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<string>(responseBody);
        }

        public Street GetValue(int id)
        {
            var response = _httpClient.GetAsync($"{APIUrl.HOST_URL}api/Streets/{id}").Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<Street>(responseBody);
        }

        public IEnumerable<Street> GetValues()
        {
            var response = _httpClient.GetAsync($"{APIUrl.HOST_URL}api/Streets").Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<IEnumerable<Street>>(responseBody);
        }

        public string Update(int id, Street value)
        {
            var data = JsonConvert.SerializeObject(value);
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = _httpClient.PutAsync($"{APIUrl.HOST_URL}api/Streets/{id}", content).Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<string>(responseBody);
        }
    }
}
