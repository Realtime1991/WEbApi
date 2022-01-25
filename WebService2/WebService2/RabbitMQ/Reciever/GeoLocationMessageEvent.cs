using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBus.Base.Standard;
using WebService2.Models;

namespace WebService2.RabbitMQ.Reciever
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
