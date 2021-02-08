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
using TkGrid;

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
        public async Task<ActionResult<GridData<WeatherForecast>>> GetWeatherForecasts(string search,
            int page = 1,
            int pageSize = 10,
            string sortColumnName = "Id",
            string filterColumnName = "Summary",
            string sortDirection = "desc"
            )
        {
            GridData<WeatherForecast> weatherForecasts = new GridData<WeatherForecast>();
            List<WeatherForecast> forecasts = new List<WeatherForecast>();

            //Expression<Func<WeatherForecast, bool>> searchCondition;
            if (!string.IsNullOrEmpty(search)) { 
                switch (filterColumnName)
                {
                    case "Summary":
                        forecasts = await _context.WeatherForecasts.Where(x => x.Summary.ToLower().Contains(search)).ToListAsync();
                        //searchCondition = x => x.Summary.Contains(search);
                        break;
                    case "Date":
                        forecasts = await _context.WeatherForecasts.Where(x => x.Date.ToString().Contains(search)).ToListAsync();
                        //searchCondition = x => x.Date.ToString().Contains(search);
                        break;
                    case "TemperatureF":
                        forecasts = await _context.WeatherForecasts.Where(x => x.TemperatureF.ToString().Contains(search)).ToListAsync();

                        //searchCondition = x => x.TemperatureF.ToString().Contains(search);
                        break;
                    case "TemperatureC":
                        forecasts = await _context.WeatherForecasts.Where(x => x.TemperatureC.ToString().Contains(search)).ToListAsync();

                        //searchCondition = x => x.TemperatureC.ToString().Contains(search);
                        break;
                    default:
                        forecasts = await _context.WeatherForecasts.Where(x => x.Summary.ToLower().ToString().Contains(search)).ToListAsync();
                        //searchCondition = x => x.Summary.Contains(search);
                        break;
                }
            } else
            {
                forecasts = await _context.WeatherForecasts.ToListAsync();
            }

            bool orderByDescending = true;
            if (sortDirection == "asc")
            {
                orderByDescending = false;
            }

            switch (sortColumnName)
            {
                case "Date":
                    if (orderByDescending)
                    {
                        forecasts = forecasts.OrderByDescending(x => x.Date).ToList();
                    }
                    else
                    {
                        forecasts = forecasts.OrderBy(x => x.Date).ToList();
                    }
                    break;
                case "Summary":
                    if (orderByDescending)
                    {
                        forecasts = forecasts.OrderByDescending(x => x.Summary).ToList();
                    }
                    else
                    {
                        forecasts = forecasts.OrderBy(x => x.Summary).ToList();
                    }
                    //forecasts = await _context.WeatherForecasts
                    //  .WhereIf(!string.IsNullOrEmpty(search), searchCondition)
                    //  .PageBy(x => x.Summary, page, pageSize, orderByDescending)
                    //  .ToListAsync();
                    break;
                default:

                    if (orderByDescending)
                    {
                        forecasts = forecasts.OrderByDescending(x => x.Id).ToList();
                    }
                    else
                    {
                        forecasts = forecasts.OrderBy(x => x.Id).ToList();
                    }
                    //forecasts = await _context.WeatherForecasts
                    //  .WhereIf(!string.IsNullOrEmpty(search), searchCondition)
                    //  .PageBy(x => x.Id, page, pageSize, orderByDescending)
                    //  .ToListAsync();
                    break;
            }
            var forest = forecasts.Count();
            forecasts = forecasts.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            weatherForecasts.Data.AddRange(forecasts);
            weatherForecasts.TotalCount = forest;//forecasts.Count(); //await _context.WeatherForecasts.WhereIf(!string.IsNullOrEmpty(search), searchCondition).CountAsync();
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
