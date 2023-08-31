using Microsoft.EntityFrameworkCore;
using WebAPITestTask.Models;

namespace WebAPITestTask.db.initialData
{
    public static class CreateLocality 
    {
        public static void Execute(ModelBuilder modelBuilder)
        {
            var lst = new List<Locality> {
                new Locality
                {
                        Id = 1,
                        Name = "Красноярск",
               
                },
                new Locality
                {
                        Id = 2,
                        Name = "Ачинск",
                   
                },
                new Locality
                {
                        Id = 3,
                        Name = "Лесосибирск,",
                     

                },
                new Locality
                {
                        Id = 4,
                        Name = "Шарыпово",
                      

                },
                new Locality
                {
                        Id = 5,
                        Name = "Дивногорск",
                        

                },
                 new Locality
                {
                        Id = 6,
                        Name = "Сосновоборск",
                     

                },
            };
            modelBuilder.Entity<Locality>().HasData(lst);


        }
    }
}
