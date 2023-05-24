using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthentication.Models
{
    public class AuthenticationResponse
    {
        public string UserName { get; set; }
        public int Expires { get; set; }
        public string token { get; set; }
    }
}
