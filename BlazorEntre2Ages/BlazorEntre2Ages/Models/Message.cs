using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorEntre2Ages.Models
{
    public class Message
    {
        public long TimeStamp { get; set; }
        public Guid Author { get; set; }
        public Guid Guest { get; set; }
        public string Body { get; set; }
        public bool Mine { get; set; }

        public bool IsNotice => Body.StartsWith("[Notice]");
        public string CSS => Mine ? "sent" : "received";
    }
}
