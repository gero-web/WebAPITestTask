 using Newtonsoft.Json;
 
namespace CLientTestTask.Models
{
    public class Apartment
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string FIOOnwer { get; set; }

      
        public int HomeId { get; set; } 
        public Home Home { get; set; }

        public override string ToString()
        {
            return this.Name;
        }



    }
}
