using Microsoft.EntityFrameworkCore;
using WebAPITestTask.db.initialData;
using WebAPITestTask.Models;

namespace WebAPITestTask.Db
{
    public class WebApiContext : DbContext
    {
        public DbSet<Locality> Locality { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<Home> Homes { get; set; }
        public DbSet<Apartment> Apartments { get; set; }

        public WebApiContext(DbContextOptions<WebApiContext> options) : base(options)
        {

            Database.EnsureCreated();
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            CreateLocality.Execute(modelBuilder);
            CreateStreet.Execute(modelBuilder);
            CreateHome.Execute(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

    }
}
