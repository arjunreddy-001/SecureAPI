using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureAPI.Models.DTOs
{
    public class AuthUserDTO
    {
        public AuthUserDTO(string firstname, string lastname, string username, string token)
        {
            FirstName = firstname;
            LastName = lastname;
            UserName = username;
            Token = token;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}