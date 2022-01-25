using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBus.Base.Standard;
using WebService2.RabbitMQ.Transmitter;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebService2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeoLocationsController : ControllerBase
    {
        private readonly IEventBus _eventBus;
        public GeoLocationsController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        //TO DO список геолокаций из файла
       // GET: api/<GeoLocationsController>
       //[HttpGet]
       // public IEnumerable<string> Get()
       // {
       //     //TO DO read from file
       //     return new string[] { "value1", "value2" };
       // }
       
    }
}
