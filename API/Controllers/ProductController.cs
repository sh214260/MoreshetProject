using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // GET: api/<ProductController>
        [HttpGet]
        //public IEnumerable<DTO.Product> Get()
        //{
        //    //return new string[] { "value1", "value2" };
        //}

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        //public DTO.Product Get(int id)
        //{
        //   // return "value";
        //}

        // POST api/<ProductController>
        [HttpPost]
        public void Post([FromBody] DTO.Product product)
        {
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] DTO.Product product)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
