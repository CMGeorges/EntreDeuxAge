using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventMicroservice.Domain.Models
{
    public class Event
    {
        [Key]
        public Guid Id { get;set;}
        public Guid AuthorId { get; set; }
        public Guid GuestId { get; set; }
        public long EpochStart { get;set;}
        public long EpochEnd { get; set; }
        public string Subject { get; set; }
        public bool Status { get;set;}
    }
}
