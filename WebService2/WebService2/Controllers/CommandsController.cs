using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService2.RabbitMQ.Transmitter;
using WebService2.Models;
using EventBus.Base.Standard;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebService2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly IEventBus _eventBus;

        public CommandsController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        // POST: api/CommandsController
        [HttpPost]
        public async Task<IActionResult> PostCommandsDestroyController(Command command)
        {

            //To do если в файле есть то выполняем действие

            var message = new CommandEvent(command);
            _eventBus.Publish(message);
            //return NotFound();
            return NoContent();
        }       
    }
}
