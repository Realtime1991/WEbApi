using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBus.Base.Standard;
using Serilog;

namespace Service1.RabbitMQ.Reciever
{
    public class CommandEventEventHandler : IIntegrationEventHandler<CommandEvent>
    {
        ILogger _logger;

        public CommandEventEventHandler(ILogger logger)
        {
            _logger = logger;
        }

        public async Task Handle(CommandEvent @event)
        {
            var itemId = @event._command.PlaneId;
            var itemString = @event._command.ActionToDo;
            _logger.Error("Plane id: {itemId}, Command: {itemString}",
                 itemId.ToString(),
                itemString
                );
        }
    }
}
