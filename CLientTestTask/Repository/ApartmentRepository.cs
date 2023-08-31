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
    
    public class ApartmentRepository : IRepository<Apartment>
    {
        private readonly HttpClient _httpClient;
        public ApartmentRepository(HttpClient httpClient)
        {
                _httpClient = httpClient; 
        }
        public Apartment CreateValue(Apartment value)
        {
            var data = JsonConvert.SerializeObject(value);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync($"{APIUrl.HOST_URL}api/Apartments", content).Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<Apartment>(responseBody);
        }

        public string Delete(int id)
        {
            var response = _httpClient.DeleteAsync($"{APIUrl.HOST_URL}api/Apartments/{id}").Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<string>(responseBody);
        }

        public Apartment GetValue(int id)
        {
            var response = _httpClient.GetAsync($"{APIUrl.HOST_URL}api/Apartments/{id}").Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<Apartment>(responseBody);
        }

        public IEnumerable<Apartment> GetValues()
        {

            var response = _httpClient.GetAsync($"{APIUrl.HOST_URL}api/Apartments").Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<IEnumerable<Apartment>>(responseBody);
        }

        public string Update(int id, Apartment value)
        {
            var data = JsonConvert.SerializeObject(value);
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = _httpClient.PutAsync($"{APIUrl.HOST_URL}api/Apartments/{id}", content).Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<string>(responseBody);
        }
    }
}
