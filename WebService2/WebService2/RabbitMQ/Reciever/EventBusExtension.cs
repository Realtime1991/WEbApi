using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBus.Base.Standard;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WebService2.Models;
using Serilog;

namespace WebService2.RabbitMQ.Reciever
{
    public static class EventBusExtension
    {        
        public static IEnumerable<IIntegrationEventHandler> GetHandlers( ILogger logger)
        {
            return new List<IIntegrationEventHandler>
        {
            new GeoLocationMessageEventHandler(logger)
        };
        }

        public static IApplicationBuilder SubscribeToEvents(this IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            eventBus.Subscribe<GeoLocationMessageEvent, GeoLocationMessageEventHandler>();

            return app;
        }
    }
}
