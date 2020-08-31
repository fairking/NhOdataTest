using System;

namespace NhOdataTest.ViewModels
{
    public class WeatherForecastVm : BaseWeatherForecastVm
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF { get; set; }

        public string Summary { get; set; }

        public string Town2 { get; set; }
    }

}
