using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBus.Base.Standard;
using WebService2.Models;

namespace WebService2.RabbitMQ.Transmitter
{
    public class CommandEvent : IntegrationEvent
    {
        public Command _command { get; set; }

        public CommandEvent(Command command)
        {
            _command = command;
        }
}
}
