using System;
using System.Runtime.Serialization;

namespace BlazorEntre2Ages.Models
{
    public class Event
    {
        public Guid Id { get;set;}
        public string Author { get; set; }
        public string Guest { get; set; }
        public long EpochStart { get;set;}
        public long EpochEnd { get; set; }
        public string Subject { get; set; }
        public bool Status { get;set;}

        public Appointment ToAppointment()
        {
            return new Appointment()
            {
                End = new DateTime(EpochEnd),
                Start = new DateTime(EpochStart),
                Subject = Subject,
                Status = Status,
                GuestEmail = Guest
            };
        }
    }
}