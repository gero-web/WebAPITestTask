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
    public class LocalityRepository : IRepository<Locality>
    {
        private readonly HttpClient _httpClient;
        public LocalityRepository(HttpClient httpClient) 
        {
                _httpClient = httpClient;
        }

        public Locality CreateValue(Locality value)
        {
            var data = JsonConvert.SerializeObject(value);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync($"{APIUrl.HOST_URL}api/Localities", content).Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<Locality>(responseBody);
        }

        public string Delete(int id)
        {
            var response = _httpClient.DeleteAsync($"{APIUrl.HOST_URL}api/Localities/{id}").Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<string>(responseBody);
        }

        public Locality GetValue(int id)
        {
            var response = _httpClient.GetAsync($"{APIUrl.HOST_URL}api/Localities/{id}").Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<Locality>(responseBody);
        }

        public IEnumerable<Locality> GetValues()
        {
            var response = _httpClient.GetAsync($"{APIUrl.HOST_URL}api/Localities").Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<IEnumerable<Locality>>(responseBody);
        }

        public string Update(int id, Locality value)
        {
            var data = JsonConvert.SerializeObject(value);
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = _httpClient.PutAsync($"{APIUrl.HOST_URL}api/Localities/{id}", content).Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<string>(responseBody);
        }
        
    } 
}
