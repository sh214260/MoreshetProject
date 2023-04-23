using Microsoft.AspNetCore.Mvc;
using DTO;
using Repositories.Models;
using Services.Interfaces;

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
        //public IEnumerable<DTO.User> Get()
        //{
        // //   return; 
        //}

        // GET api/<User>/5
        [HttpGet("{id}")]
        //public DTO.User Get(int id)
        //{
        //   // return "value";
        //}

        // POST api/<User>
        [HttpPost]
        public bool Post([FromBody] DTO.User user)
        {
             
            return service.AddNew(user);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] DTO.User user)
        {
        }

        // DELETE api/<User>/5
        [HttpDelete("{id}")]
        public void Delete(int userId)
        {
        }
    }
}
