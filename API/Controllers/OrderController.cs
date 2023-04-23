using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        // GET: api/<OrderController>
        [HttpGet]
        //public IEnumerable<DTO.Order> Get()
        //{
        //   // return new string[] { "value1", "value2" };
        //}

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        //public DTO.Order Get(int id)
        //{
        // //   return "value";
        //}

        // POST api/<OrderController>
        [HttpPost]
        public void Post([FromBody] DTO.Order order)
        {
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] DTO.Order order)
        {
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
