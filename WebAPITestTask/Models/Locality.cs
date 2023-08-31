using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace WebAPITestTask.Models
{
    public class Locality
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Street> Street { get; set; } = new List<Street>();

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
