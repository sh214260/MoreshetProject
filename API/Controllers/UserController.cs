using Microsoft.AspNetCore.Mvc;
using DTO;
using Repositories.Models;
using Services.Interfaces;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

//using System.Web.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Services.Interfaces.IUserService service;
        private readonly Services.Interfaces.ICartService cartservice;
        private readonly Services.Interfaces.ICartProductService cartProductservice;
        private readonly IConfiguration configuration;
        public UserController(IUserService bl, ICartService cartservice, ICartProductService cartProductservice, IConfiguration configuration)
        {
            this.configuration = configuration;
            service = bl;
            this.cartservice= cartservice;
            this.cartProductservice = cartProductservice;

        }

        // GET: api/<User>
        [HttpGet]
        public IEnumerable<DTO.User> Get()
        {
            IEnumerable <DTO.User> data = service.Get();
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return data;

        }

        // GET api/<User>/5
        [HttpGet("{id}")]
        public DTO.User Get(int id)
        {
          return service.Get(id);
        }
        
        [HttpGet("Profile")]
        [Authorize]
        public IActionResult GetProfile()
       {
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:3000");
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                // Now you have the USER.ID in the userId variable
                // You can use this userId to make a request to a service
                var userDetails = service.Get(userId);

                if (userDetails != null)
                {
                    return Ok(userDetails); // Return user details from the service
                }
                else
                {
                    return NotFound("User details not found");
                }
            }
            else
            {
                return BadRequest("Unable to extract User ID from token");
            }
        }
       
        // GET api/<User>/5
        [HttpPost("signin")]
        public DTO.LoginResponse Singin([FromBody] Login login)
        {
            DTO.User user = new DTO.User();
            if (login.by == "client" && login.email != "" && login.password != "")
            {
                user = service.GetUser(login.email, login.password);
                if (user == null)
                {
                    throw new System.Web.Http.HttpResponseException(System.Net.HttpStatusCode.Unauthorized);
                }
            }
            else
            {
                user = service.GetUserByPhone(login.phonenumber);
                if (user == null)
                {
                    throw new System.Web.Http.HttpResponseException(System.Net.HttpStatusCode.Unauthorized);
                }
            }
            DTO.Cart cart = cartservice.GetByUser(user.Id);
            if (cart == null)
            {
                throw new System.Web.Http.HttpResponseException(System.Net.HttpStatusCode.Unauthorized);
            }
            IEnumerable<DTO.Product> cartProducts = cartProductservice.GetProducts(cart.Id);
            if (cartProducts == null)
            {
                LoginResponse respo = new LoginResponse()
                {
                    User = user,
                    Cart = cart,
                    CartProducts = null,
                };
                return respo;
            }
            //JWT
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
            var signingCredentials = new SigningCredentials(
                                    new SymmetricSecurityKey(key),
                                    SecurityAlgorithms.HmacSha512Signature
                                );
            var subject = new ClaimsIdentity(new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            });
            var expires = DateTime.UtcNow.AddHours(1);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = expires,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = signingCredentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            LoginResponse response = new LoginResponse()
            {
                User = user,
                Cart = cart,
                CartProducts = cartProducts,
                Token = jwtToken
            };
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return response;
        }


        // POST api/<User>
        [HttpPost("signup/{password}")]
        //[EnableCors("AllowAllOrigins")]
        public bool SignUp(string password,[FromBody] DTO.User user)
        {
            bool data = service.AddNew(password,user);
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return data;
            
        }
        [HttpPost("updateuser")]
        //[EnableCors("AllowAllOrigins")]
        public bool UpdateUser([FromBody] DTO.User user)
        {
            bool response = service.UpdateUser(user);
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return response;
        }
        
        // DELETE api/<User>/5
        [HttpDelete("{userId}")]
        public void Delete(int userId)
        {
            service.Delete(userId);
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");           
        }
    }
}
