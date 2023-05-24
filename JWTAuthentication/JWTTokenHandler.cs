using JWTAuthentication.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthentication
{
    public class JWTTokenHandler
    {
        public const string JWT_SECURITY_KEY = "jagathprasadkarrapaulinepreethikarra";
        public const int JWT_TOKEN_VALIDITY_MINS = 20;
        public List<UserAccount> userList;
        public JWTTokenHandler()
        {
            userList = new List<UserAccount>
            {
                new UserAccount{UserName="jagath",Password="12345",Role="Admin"},
                new UserAccount{UserName="prasad",Password="12345",Role="User"},
            };
        }

        public AuthenticationResponse GenerateJWTToken(AuthenticationRequest request)
        {
            if (!string.IsNullOrEmpty(request.UserName) && !string.IsNullOrEmpty(request.Password))
            {
                var userAccount = userList.Where(x => x.UserName.Equals(request.UserName) && x.Password.Equals(request.Password)).FirstOrDefault();
                if (userAccount != null)
                {
                    var tokenExpiry = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
                    var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
                    var claimsIdentity = new ClaimsIdentity(new List<Claim> {
                        new Claim(JwtRegisteredClaimNames.Name, request.UserName),
                        new Claim(ClaimTypes.Role,userAccount.Role)
                    });

                    var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature);
                    var securityTokenDescriptor = new SecurityTokenDescriptor { Subject = claimsIdentity, Expires = tokenExpiry, SigningCredentials = signingCredentials };

                    var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
                    var token = jwtSecurityTokenHandler.WriteToken(securityToken);
                    return new AuthenticationResponse
                    {
                        UserName = userAccount.UserName,
                        Expires = (int)tokenExpiry.Subtract(DateTime.Now).TotalSeconds,
                        token = token
                    };
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }
    }
}
