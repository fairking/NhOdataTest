using Microsoft.AspNetCore.Mvc;
using NHibernate;
using NHibernate.Linq;
using NhOdataTest.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace NhOdataTest.Controllers
{
    public class WeatherForecastController : ControllerBase
    {
        private readonly ISession _session;

        public WeatherForecastController(ISession session)
        {
            _session = session;
        }

        [HttpGet]
        public async Task<IActionResult> GetDewPoints()
        {
            // Reproducing the issue https://github.com/nhibernate/nhibernate-core/issues/2470

            var forecast = await _session.Query<WeatherForecast>().FirstOrDefaultAsync();

            var dewPoints = forecast.DewPoints.Where(x => x.Type == DewPointTypeEnum.Normal).ToList();

            return Ok(new { Count = dewPoints.Count });
        }

    }
}
