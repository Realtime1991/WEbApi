using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService2.Models;
using EventBus.Base.Standard;
using Serilog;
namespace WebService2.RabbitMQ.Reciever
{
    public class GeoLocationMessageEventHandler : IIntegrationEventHandler<GeoLocationMessageEvent>
    {
        ILogger _logger;
        public GeoLocationMessageEventHandler(ILogger logger)
        {
            _logger = logger;
        }

        public async Task Handle(GeoLocationMessageEvent @event)
        {
            var itemId = @event._planeGeoLocation.Id;
            var itemLatitude = @event._planeGeoLocation.Latitude;
            var itemLongitude = @event._planeGeoLocation.Longitude;
            var itemAltitude = @event._planeGeoLocation.Altitude;            
            
            _logger.Error(
                "Plane detected id: {itemId}, Lat: {itemLatitude}, Long: {itemLongitude}, Alt: {itemAltitude}",
                itemId.ToString(),
                itemLatitude.ToString(),
                itemLongitude.ToString(),
                itemAltitude.ToString());

        }
    }
}
