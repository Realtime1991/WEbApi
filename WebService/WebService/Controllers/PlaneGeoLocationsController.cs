using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventBus.Base.Standard;

using Service1.Models;
using Service1.RabbitMQ.Transmitter;
using Serilog;

namespace Service1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaneGeoLocationsController : ControllerBase
    {
        private readonly PlaneGeoLocationContext _context;
        private readonly IEventBus _eventBus;
        private readonly ILogger _logger;

        public PlaneGeoLocationsController(PlaneGeoLocationContext context, IEventBus eventBus, ILogger logger)
        {
            _eventBus = eventBus;
            _context = context;
            _logger = logger;
        }

        // GET: api/PlaneGeoLocations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaneGeoLocation>>> GetPlaneGeoLocationItems()
        {
            //to do delete (for tests)
            //var planeGeoLocation = await _context.PlaneGeoLocationItems.FindAsync((long)1);
            //var message = new GeoLocationMessageEvent(planeGeoLocation);
            //_eventBus.Publish(message);

            return await _context.PlaneGeoLocationItems.ToListAsync();
        }

        // GET: api/PlaneGeoLocations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlaneGeoLocation>> GetPlaneGeoLocation(long id)
        {
            var planeGeoLocation = await _context.PlaneGeoLocationItems.FindAsync(id);

            if (planeGeoLocation == null)
            {
                return NotFound();
            }

            return planeGeoLocation;
        }

        // POST: api/PlaneGeoLocations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PlaneGeoLocation>> PostPlaneGeoLocation(PlaneGeoLocation planeGeoLocation)
        {
            _context.PlaneGeoLocationItems.Add(planeGeoLocation);
            await _context.SaveChangesAsync();

            var message = new GeoLocationMessageEvent(planeGeoLocation);
            _eventBus.Publish(message);

            return CreatedAtAction("GetPlaneGeoLocation", new { id = planeGeoLocation.Id }, planeGeoLocation);
        }

        // PUT: api/PlaneGeoLocations1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlaneGeoLocation(long id, PlaneGeoLocation planeGeoLocation)
        {
            if (id != planeGeoLocation.Id)
            {
                return BadRequest();
            }

            _context.Entry(planeGeoLocation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                var message = new GeoLocationMessageEvent(planeGeoLocation);
                _eventBus.Publish(message);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaneGeoLocationExists(id))
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
        // DELETE: api/PlaneGeoLocations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaneGeoLocation(long id)
        {
            var planeGeoLocation = await _context.PlaneGeoLocationItems.FindAsync(id);
            if (planeGeoLocation == null)
            {
                return NotFound();
            }

            _context.PlaneGeoLocationItems.Remove(planeGeoLocation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlaneGeoLocationExists(long id)
        {
            return _context.PlaneGeoLocationItems.Any(e => e.Id == id);
        }
    }
}
