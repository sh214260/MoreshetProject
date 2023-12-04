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

        [HttpGet("productisavialible")] 
        public int Get([FromQuery] ProductToOrder pro)
        {
            List<int> productIds= service.ProductIsAvialible(pro.ProductType, pro.From, pro.To);
            if (productIds.Count()==0)
            {
                //המוצר תפוס
                return 0;
            } 
            foreach (int productId in productIds)
            {
                int id = service.AddToCart(pro.UserId, productId, pro.From, pro.To);
                if (id != -1)
                {
                    return id;
                }
            }
            //קיים מוצר כזה בעגלה
            return -1;        
        }
        [HttpPost("updatedate")]
        //[EnableCors("AllowAllOrigins")]
        public bool Post(int cartId, DateTime from, DateTime to)
        {
            return service.UpdateDate(cartId,from, to);
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
