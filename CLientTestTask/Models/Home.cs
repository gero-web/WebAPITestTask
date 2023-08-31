 
using Newtonsoft.Json;
 
namespace CLientTestTask.Models
{
    public class Home
    { 

        public int Id { get; set; }
        
        public string Name { get; set; }


       
        public int StreetId { get; set; }
        public virtual Street Street { get; set; }

       
        public int CountApartments { get; set; }

      
        public override string ToString()
        {
            return this.Name;
        }



    }
}
