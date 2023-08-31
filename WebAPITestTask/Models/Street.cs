using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPITestTask.Models
{
    public class Street
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int LocalityId { get; set; }

       
        public Locality Locality { get; set; } = null!;

        [JsonIgnore]
        public ICollection<Home> Homes { get; } = new List<Home>();

        public override bool Equals(object? obj)
        {
            if (obj == null) 
                return this.Name.IsNullOrEmpty();
            return this.Name.Equals(obj);
        }

        public override string ToString()
        {
            return this.Name.ToString();
        }
    }
}
