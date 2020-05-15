using System;

namespace NhOdataTest.Entities
{
    public class WeatherForecast
    {
        protected WeatherForecast()
        {
        }

        public WeatherForecast(Town town)
        {
            if (town == null)
                throw new ArgumentNullException(nameof(town));

            Town = town;
        }

        public virtual Guid Id { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual int TemperatureC { get; set; }

        public virtual string Summary { get; set; }

        public virtual Town Town { get; protected set; }
    }
}
