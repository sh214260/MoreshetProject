using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using DTO;
using Repositories.Models;
using Services;
using Microsoft.AspNetCore.Cors;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartContoller : ControllerBase
    {
        private readonly Services.Interfaces.ICartService service;
        public CartContoller(ICartService bl)
        {
            service = bl;
        }
        // GET: api/<CartCartoller>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //GET api/<CartCartoller>/5
        [HttpGet("{cartid}")]
        public int GetTotalPrice(int cartid)
        {
            return service.GetTotalPrice(cartid);
        }

        //addtoCart
        //PUT api/<CartController>/5
        [HttpPut("{productid}")]
        public DTO.Cart Put(int productid, [FromBody] DTO.User user )
        {
            return service.AddToCart(user.Id, productid);
        }
        // POST api/<CartCartoller>
        [HttpGet("productisavialible")]
        public bool ProductIsAvialible([FromQuery] ProductToOrder pro)
        {
            return service.ProductIsAvialible(pro.Type, pro.from, pro.to);
        }

        // PUT api/<CartCartoller>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<CartCartoller>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
