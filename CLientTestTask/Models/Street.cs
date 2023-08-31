using Newtonsoft.Json;
using System.Collections.Generic;

namespace CLientTestTask.Models
{
    public class Street
    {
        public int Id { get; set; }

        public string Name { get; set; }

      
        public int LocalityId { get; set; }
        public Locality Locality { get; set; }  

         

        public override string ToString()
        {
            return this.Name;
        }

    }
}
