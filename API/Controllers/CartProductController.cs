using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartProductController : ControllerBase
    {
        private readonly Services.Interfaces.ICartProductService service;
        public CartProductController(ICartProductService bl)
        {
            service = bl;
        }
        // GET: api/<CartProductController1>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        [HttpGet("{cartId}")]
        public IEnumerable<DTO.CartProduct> Get(int cartId)
        {
            IEnumerable<DTO.CartProduct> data = service.Get(cartId);
            return data;
        }

        [HttpGet("getproducts/{cartId}")]
        [Authorize]
        public IEnumerable<DTO.Product> GetProducts(int cartId)
        {
            IEnumerable<DTO.Product> data = service.GetProducts(cartId);
            return data;
        }
        // GET api/<CartProductController1>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<CartProductController1>
        [HttpPost("delete/{cartId}/{productId}")]
        [Authorize]
        public bool Post(int cartId, int productId)
        {
            return service.Delete(cartId, productId);
        }

        //// PUT api/<CartProductController1>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<CartProductController1>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
