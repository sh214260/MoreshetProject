using Microsoft.AspNetCore.Mvc;
using DTO;
using Repositories.Models;
using Services.Interfaces;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Services.Interfaces.IUserService service;
        public UserController(IUserService bl)
        { 
            service = bl;
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

        // GET api/<User>/5
        [HttpPost("signin")]
        public DTO.User Singin([FromBody] Login login)
        {
            DTO.User data = service.GetUser(login.email, login.password);
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return data;
        }


        // POST api/<User>
        [HttpPost("signup")]
        //[EnableCors("AllowAllOrigins")]
        public bool Post([FromBody] DTO.User user)
        {
            bool data = service.AddNew(user);
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return data;
            
        }

        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] DTO.User user)
        //{
            
        //}

        // DELETE api/<User>/5
        [HttpDelete("{userId}")]
        public void Delete(int userId)
        {
            service.Delete(userId);
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
           
            
        }
    }
}
