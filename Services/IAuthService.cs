using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecureAPI.Models;

namespace SecureAPI.Services
{
    public interface IAuthService
    {
        AuthUser Authenticate(string username, string password);
    }
}