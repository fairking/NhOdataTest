using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NhOdataTest.Entities
{
    public class DewPoint
    {
        protected DewPoint() { }

        public DewPoint(WeatherForecast forecast, DateTime time, DewPointTypeEnum type)
        {
            if (time == DateTime.MinValue)
                throw new ArgumentNullException(nameof(time));

            Forecast = forecast ?? throw new ArgumentNullException(nameof(forecast));
            Time = time;
            Type = type;
        }

        public virtual Guid Id { get; protected set; }
        public virtual DateTime Time { get; protected set; }
        public virtual DewPointTypeEnum Type { get; protected set; }
        public virtual WeatherForecast Forecast { get; protected set; }
    }

    public enum DewPointTypeEnum
    {
        Low,
        Normal,
        High,
    }
}
