using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        // GET: api/<CategoryController>
        [HttpGet]
        //public IEnumerable<DTO.Category> Get()
        //{
        //   // return new string[] { "value1", "value2" };
        //}

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        //public DTO.Category Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<CategoryController>
        [HttpPost]
        public void Post([FromBody] DTO.Category category)
        {
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] DTO.Category category)
        {
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
