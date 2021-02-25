using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQBasicReceiver.Models
{
    public class Message
    {
        public long TimeStamp { get; set; }
        public Guid Author { get; set; }
        public Guid Guest { get; set; }
        public string Body { get; set; }
        public bool Mine { get;set;}
    }
}
