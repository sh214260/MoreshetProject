using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsForOrderController : ControllerBase
    {
        private readonly Services.Interfaces.IItemForOrderService service;
        public ItemsForOrderController(IItemForOrderService bl)
        {
            service = bl;
        }
        // GET: api/<ItemsForOrderController>
        [HttpGet]
        //public IEnumerable<DTO.ItemsForOrder> Get()
        //{
        //    //return new string[] { "value1", "value2" };
        //}

        // GET api/<ItemsForOrderController>/5
        [HttpGet("{id}")]
        //public DTO.ItemsForOrder Get(int id)
        //{
        //    //return "value";
        //}

        // POST api/<ItemsForOrderController>
        [HttpPost("addtocart")]
        public void Post([FromBody] DTO.ItemsForOrder itemsForOrder)
        {
        }

        // PUT api/<ItemsForOrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] DTO.ItemsForOrder itemsForOrder)
        {
        }

        // DELETE api/<ItemsForOrderController>/5
        [HttpDelete("{orderId}/{productId}")]
        [Authorize]
        public bool Delete(int orderId, int productId)
        {
            return service.Delete(orderId, productId);
        }
    }
}
