using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureAPI.Models
{
    public class AuthUser
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}