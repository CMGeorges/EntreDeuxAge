using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMicroservice.Domain.Models
{
    public class PostgresConfiguration : IPostgresConfiguration
    {
        public string Host { get ; set; }
        public int Port { get ; set ; }
        public string Username { get ; set ; }
        public string Password { get ; set ; }
        public string Database { get ; set ; }
    }

    public interface IPostgresConfiguration
    {
        string Host { get;set;}
        int Port { get;set; }
        string Username { get;set;}
        string Password { get;set;}
        string Database { get;set;}
    }
}
