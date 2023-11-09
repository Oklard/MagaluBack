using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagaluAPI.Models
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}