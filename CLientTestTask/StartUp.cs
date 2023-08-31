using CLientTestTask.Models;
using CLientTestTask.Repository;
using CLientTestTask.Repository.Bases;
using CLientTestTask.View;
using CLientTestTask.ViewModel;
using CLientTestTask.ViewModel.ComadsBase;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;

namespace CLientTestTask
{
    internal class StartUp
    {
        [STAThread ]
        public static void Main()
        {
            var builder = Host.CreateDefaultBuilder()
                .ConfigureServices(service => {
                    service.AddSingleton<App>();
                    service.AddSingleton<MainWindow>();

                    service.AddSingleton<HttpClient>();
                    service.AddScoped<IRepository<Locality>, LocalityRepository>();
                    service.AddScoped<IRepository<Street>, StreetRepository>();
                    service.AddScoped<IRepository<Home>, HomeRepository>();
                    service.AddScoped<IRepository<Apartment>, ApartmentRepository>();

                    service.AddSingleton<LocalityViewModel>();
                    service.AddSingleton<StreetViewModelcs>();
                    service.AddSingleton<HomeViewModel>();
                    service.AddSingleton<ApartamentViewModel>();

                    service.AddSingleton<HomePage>();
                    service.AddTransient<HomeEditPage>();
                    service.AddTransient<StreetEditPage>();
                    service.AddTransient<LocalytiEditPage>();
                    service.AddTransient<ApartamentEditPage>();


                    service.AddSingleton<NavigatedViewModel>();
                    
                })
                .Build();

            var app = builder.Services.GetService<App>();
            app?.Run();
                
        }
    }
}
