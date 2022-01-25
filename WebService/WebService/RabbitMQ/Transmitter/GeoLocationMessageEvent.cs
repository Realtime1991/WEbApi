using EventBus.Base.Standard;
using Service1.Models;

namespace Service1.RabbitMQ.Transmitter
{
    public class GeoLocationMessageEvent : IntegrationEvent
    {
        public PlaneGeoLocation _planeGeoLocation { get; set; }
        public GeoLocationMessageEvent(PlaneGeoLocation planeGeoLocation)
        {
            _planeGeoLocation = planeGeoLocation;
        }
    }
}