using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tk.BlazorGrid.Server.Data;
using Tk.BlazorGrid.Shared;
using Tk.BlazorGrid.Shared.Extensions;

namespace Tk.BlazorGrid.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WeatherForecastController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Weather
        [HttpGet]
        public async Task<ActionResult<WeatherForecastsDto>> GetWeatherForecasts(string search,
            int page = 1,
            int pageSize = 10,
            string sortColumnName = "Id",
            string sortDirection = "desc"
            )
        {
            WeatherForecastsDto weatherForecasts = new WeatherForecastsDto();
            List<WeatherForecast> forecasts = new List<WeatherForecast>();

            Expression<Func<WeatherForecast, bool>> searchCondition = x => x.Summary.Contains(search);
            bool orderByDescending = true;
            if (sortDirection == "asc")
            {
                orderByDescending = false;
            }

            switch (sortColumnName)
            {
                case "Date":
                    forecasts = await _context.WeatherForecasts
                      .WhereIf(!string.IsNullOrEmpty(search), searchCondition)
                      .PageBy(x => x.Date, page, pageSize, orderByDescending)
                      .ToListAsync();
                    break;
                case "Summary":
                    forecasts = await _context.WeatherForecasts
                      .WhereIf(!string.IsNullOrEmpty(search), searchCondition)
                      .PageBy(x => x.Summary, page, pageSize, orderByDescending)
                      .ToListAsync();
                    break;
                default:
                    forecasts = await _context.WeatherForecasts
                      .WhereIf(!string.IsNullOrEmpty(search), searchCondition)
                      .PageBy(x => x.Id, page, pageSize, orderByDescending)
                      .ToListAsync();
                    break;
            }

            weatherForecasts.WeatherForecasts.AddRange(forecasts);
            weatherForecasts.TotalCount = await _context.WeatherForecasts.WhereIf(!string.IsNullOrEmpty(search), searchCondition).CountAsync();
            weatherForecasts.PageSize = pageSize;

            return weatherForecasts;
        }

        // GET: api/Weather/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WeatherForecast>> GetWeatherForecast(Guid id)
        {
            var weatherForecast = await _context.WeatherForecasts.FindAsync(id);

            if (weatherForecast == null)
            {
                return NotFound();
            }

            return weatherForecast;
        }

        // PUT: api/Weather/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeatherForecast(Guid id, WeatherForecast weatherForecast)
        {
            if (id != weatherForecast.Id)
            {
                return BadRequest();
            }

            _context.Entry(weatherForecast).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeatherForecastExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Weather
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<WeatherForecast>> PostWeatherForecast(WeatherForecast weatherForecast)
        {
            _context.WeatherForecasts.Add(weatherForecast);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWeatherForecast", new { id = weatherForecast.Id }, weatherForecast);
        }

        // DELETE: api/Weather/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WeatherForecast>> DeleteWeatherForecast(Guid id)
        {
            var weatherForecast = await _context.WeatherForecasts.FindAsync(id);
            if (weatherForecast == null)
            {
                return NotFound();
            }

            _context.WeatherForecasts.Remove(weatherForecast);
            await _context.SaveChangesAsync();

            return weatherForecast;
        }

        private bool WeatherForecastExists(Guid id)
        {
            return _context.WeatherForecasts.Any(e => e.Id == id);
        }
    }
}
