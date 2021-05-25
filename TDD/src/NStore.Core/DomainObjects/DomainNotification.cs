using MediatR;
using NStore.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStore.Core.DomainObjects
{
    public class DomainNotification : Message, INotification
    {
        public DomainNotification(string key, string value)
        {
            Timestamp = DateTime.Now;
            DomainNotificationId = Guid.NewGuid();
            Key = key;
            Value = value;
            Version = 1;
        }

        public DateTime Timestamp { get; private set; }
        public Guid DomainNotificationId { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }
        public int Version { get; private set; }


    }
}
