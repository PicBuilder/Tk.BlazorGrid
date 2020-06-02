using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Tk.BlazorGrid.Shared;

namespace Tk.BlazorGrid.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    }

    public class SeedData
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
               serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
                {
                    // Look for any weatherforecasts.
                    if (context.WeatherForecasts.Any())
                    {
                        return;   // Data was already seeded
                    }

                    var rng = new Random();

                    var weatherForecasts = Enumerable.Range(1, 1000).Select(index => new WeatherForecast
                    {
                        Id = new Guid(),
                        Date = DateTime.Now.AddDays(index),
                        TemperatureC = rng.Next(-20, 55),
                        Summary = Summaries[rng.Next(Summaries.Length)]
                    })
                    .ToList();

                    context.WeatherForecasts.AddRange(weatherForecasts);

                    context.SaveChanges();
                }
        }
    }

}
