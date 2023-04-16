using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsForOrderController : ControllerBase
    {
        // GET: api/<ItemsForOrderController>
        [HttpGet]
        public IEnumerable<DTO.ItemsForOrder> Get()
        {
            //return new string[] { "value1", "value2" };
        }

        // GET api/<ItemsForOrderController>/5
        [HttpGet("{id}")]
        public DTO.ItemsForOrder Get(int id)
        {
            //return "value";
        }

        // POST api/<ItemsForOrderController>
        [HttpPost]
        public void Post([FromBody] DTO.ItemsForOrder itemsForOrder)
        {
        }

        // PUT api/<ItemsForOrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] DTO.ItemsForOrder itemsForOrder)
        {
        }

        // DELETE api/<ItemsForOrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
