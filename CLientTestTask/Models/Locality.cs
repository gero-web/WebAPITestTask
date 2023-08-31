using Newtonsoft.Json;
using System.Collections.Generic;

namespace CLientTestTask.Models
{
    public class Locality
    {
        public int Id { get; set; }

        public string Name { get; set; }

    
        public override string ToString()
        {
            return this.Name;
        }


    }
}
