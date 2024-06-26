﻿using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using DTO;
using Repositories.Models;
using Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
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
        //[Authorize]
        public DTO.Cart Get(int id)
        {
            return service.Get(id);
        }
        [HttpGet("getcartbyuser/{userId}")]
        
        public DTO.Cart GetByUser(int userId)
        {
            return service.GetByUser(userId);
        }

        [HttpGet("productisavialible")]
        [Authorize]
        public int Get([FromQuery] ProductToOrder pro)
        {
            List<int> productIds= service.ProductIsAvialible(pro.ProductId,pro.ProductType, pro.From, pro.To);
            if (productIds.Count()==0)
            {
                //the product is occupied
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
            //there is the same product
            return -1;        
        }
        [HttpPost("updatedate/{cartId}/{from}/{to}")]
        [Authorize]
        public bool Post(int cartId, DateTime from, DateTime to)
        {
            return service.UpdateDate(cartId, from, to);
        }
       
    }
}
