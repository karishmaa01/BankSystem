using BankSystem.Domain.Data;
using BankSystem.Domain.Jwt;
using BankSystem.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankSystem.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class JWTAuthentication : ControllerBase
    {
        private readonly BankDb _ctx;
        private readonly JWTSettings _jwtsettings;

        public JWTAuthentication(BankDb customerctx, IOptions<JWTSettings> options)
        {
            _ctx = customerctx;
            _jwtsettings = options.Value;
        }

        [AllowAnonymous]
        [HttpPost("Authentication")]

        public IActionResult Authentication([FromBody] Login customer)
        {
            var _customer = _ctx.Customers.FirstOrDefault(l => l.Email == customer.Email && l.Password == customer.Password);
            if (_customer == null)
                return Unauthorized();

            var tokenhandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.UTF8.GetBytes(_jwtsettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name,_customer.CustomerName.ToString()),
                    }
                    ),
                Expires = DateTime.Now.AddSeconds(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenhandler.CreateToken(tokenDescriptor);
            string finaltoken = tokenhandler.WriteToken(token);
            return Ok(finaltoken);
        }
    }
}
