using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBus.Base.Standard;
using Service1.Models;

namespace Service1.RabbitMQ.Reciever
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
