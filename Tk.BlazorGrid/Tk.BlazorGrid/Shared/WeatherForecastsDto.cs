using System;
using System.Collections.Generic;
using System.Text;

namespace Tk.BlazorGrid.Shared
{
    public class WeatherForecastsDto
    {
		public WeatherForecastsDto()
		{
			WeatherForecasts = new List<WeatherForecast>();
		}

		public int PageSize { get; set; }

		public int TotalCount { get; set; }

		public List<WeatherForecast> WeatherForecasts { get; set; }
	}
}
