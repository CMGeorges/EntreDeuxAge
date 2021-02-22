using System;
using System.ComponentModel.DataAnnotations;

namespace UserMicroservice.Domain.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        [Phone]
        public string Phone { get; set; }
        [Phone]
        public string Phone2 { get; set; }
    }
}