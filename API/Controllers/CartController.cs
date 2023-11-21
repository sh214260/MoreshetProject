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
    public class CartController : ControllerBase
    {
        private readonly Services.Interfaces.ICartService service;
        public CartController(ICartService bl)
        {
            service = bl;
        }

        [HttpGet("getbyid/{id}")]
        public DTO.Cart Get(int id)
        {
            return service.Get(id);
        }
        //addtoCart
        //PUT api/<CartController>/5
        //[HttpPut("{productid}")]
        //public int Put(int productid)
        //{
        //    return service.AddToCart(1, productid);
        //}
        [HttpGet("productisavialible")] 
        public int Get([FromQuery] ProductToOrder pro)
        {
            return service.ProductIsAvialible(pro.UserId,pro.ProductId, pro.From, pro.To);
        }

        // GET api/<CartCartoller>
        [HttpGet("gettotalprice/{cartid}")]
        public double GetTotalPrice(int cartId)
        {
            return service.GetTotalPrice(cartId);
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
