using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json; 
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;

namespace WebAPITestTask.Models
{
    public class Home
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        
        public int StreetId { get; set; }

      
        public virtual Street? Street { get; set; }

        [IntegerValidator(MinValue =1 , MaxValue = 100)]
        public int CountApartments { get; set; }

        [JsonIgnore]
        public ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return this.Name.IsNullOrEmpty();

            return this.Name.Equals(obj) ;
        }

        public override string ToString()
        {
            return this.Name.ToString();
        }

    }
}
