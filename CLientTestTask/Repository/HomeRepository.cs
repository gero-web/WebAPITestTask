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
    public class HomeRepository : IRepository<Home>
    {
        private readonly HttpClient _httpClient;
        public HomeRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public Home CreateValue(Home value)
        {
            var data = JsonConvert.SerializeObject(value) ;
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync($"{APIUrl.HOST_URL}api/Homes",content).Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<Home>(responseBody);
        }

        public string Delete(int id)
        {
            var response = _httpClient.DeleteAsync($"{APIUrl.HOST_URL}api/Homes/{id}").Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<string>(responseBody);
        }

        public Home GetValue(int id)
        {
            var response = _httpClient.GetAsync($"{APIUrl.HOST_URL}api/Homes/{id}").Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<Home>(responseBody);
        }

        public  IEnumerable<Home> GetValues()
        {

            var response = _httpClient.GetAsync($"{APIUrl.HOST_URL}api/Homes").Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;
            
            return JsonConvert.DeserializeObject<IEnumerable<Home>>(responseBody);
        }

        public string Update(int id, Home value)
        {
            var data = JsonConvert.SerializeObject(value);
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = _httpClient.PutAsync($"{APIUrl.HOST_URL}api/Homes/{id}", content).Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<string>(responseBody);
        }
    }
}
