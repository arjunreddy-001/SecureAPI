using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SecureAPI.Models;

namespace SecureAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtConfig _jwtConfig;

        public AuthService(IOptions<JwtConfig> jwtConfig)
        {
            _jwtConfig = jwtConfig.Value;
        }

        private List<AuthUser> users = new List<AuthUser>() {
            new AuthUser{UserId=1, FirstName="Arjun", LastName="Reddy", UserName="arjunreddy", Password="arjunpassword"},
            new AuthUser{UserId=1, FirstName="Bharath", LastName="S", UserName="bharath", Password="bpassword"},
            new AuthUser{UserId=1, FirstName="Punith", LastName="Kumar", UserName="punithk", Password="pass@word"},
            new AuthUser{UserId=1, FirstName="Saraswathi", LastName="Devi", UserName="sdevi", Password="veena@1234"}
        };

        public AuthUser Authenticate(string username, string password)
        {
            var user = users.SingleOrDefault(x => x.UserName == username && x.Password == password);

            // return null if user not found
            if(user == null) 
            {
                return null;
            }

            // If user found - Generate Token, Add Token to "user" & remove Password from "user"
            // Then return user
            user.Token = GenerateToken(user);
            user.Password = null;

            return user;
        }

        private string GenerateToken(AuthUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Key);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.Version, "V3.1")
                }),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _jwtConfig.Issuer,
                Audience = _jwtConfig.Audience
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}