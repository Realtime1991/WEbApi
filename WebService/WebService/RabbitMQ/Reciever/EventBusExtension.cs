using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBus.Base.Standard;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Service1.Models;
using Serilog;

namespace Service1.RabbitMQ.Reciever
{
    public static class EventBusExtension
    {
        public static IEnumerable<IIntegrationEventHandler> GetHandlers(ILogger logger)
        {
            return new List<IIntegrationEventHandler>
        {
            new CommandEventEventHandler(logger)
        };
        }

        public static IApplicationBuilder SubscribeToEvents(this IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            eventBus.Subscribe<CommandEvent, CommandEventEventHandler>();

            return app;
        }
    }
}
