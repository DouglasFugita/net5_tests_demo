using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStore.Core.Messages
{
    public abstract class Message
    {
        public string MessageType { get; private set; }
        public Guid AggregateId { get; private set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }

    public abstract class Event : Message, INotification
    {
        public DateTime TimeStamp { get; private set; }
        protected Event()
        {
            TimeStamp = DateTime.Now;
        }
    }
}
