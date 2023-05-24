
using JWTAuthentication;
using JWTAuthentication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticateWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {


        private readonly JWTTokenHandler jWTTokenHandler;

        public AccountController(JWTTokenHandler _jWTTokenHandler)
        {
            this.jWTTokenHandler = _jWTTokenHandler;
        }


        [HttpPost]

        public IActionResult Authentication([FromBody] AuthenticationRequest request)
        {
            try
            {

                var authenticate = jWTTokenHandler.GenerateJWTToken(request);
                if (authenticate == null)
                    return Unauthorized();
                return Ok(authenticate);
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }
    }
}
