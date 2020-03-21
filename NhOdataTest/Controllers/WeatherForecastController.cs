using AutoMapper;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using NhOdataTest.Entities;
using NhOdataTest.ViewModels;

namespace NhOdataTest.Controllers
{
    [Route("/odata/[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ISession _session;
        private readonly IMapper _mapper;

        public WeatherForecastController(ISession session, IMapper mapper)
        {
            _session = session;
            _mapper = mapper;
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_mapper.ProjectTo<WeatherForecastVm>(_session.Query<WeatherForecast>()));
        }
    }
}
