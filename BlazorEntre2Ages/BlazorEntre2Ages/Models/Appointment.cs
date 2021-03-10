using System;
using System.Runtime.Serialization;

namespace BlazorEntre2Ages.Models
{
    public class Appointment
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Subject { get; set; }
        public bool Status { get; set; }
        public string GuestEmail { get; set; }

        public Appointment()
        {
            
        }
        public Appointment(Event @event)
        {
            Start = new DateTime(@event.EpochStart);
            End = new DateTime(@event.EpochEnd);
            Subject = @event.Subject;
            Status = @event.Status;
            GuestEmail = @event.Guest;
        }
    }
}