﻿using Microsoft.AspNetCore.Mvc;
using DTO;
using Repositories.Models;
using Services.Interfaces;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
//using System.Web.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Services.Interfaces.IUserService service;
        private readonly Services.Interfaces.ICartService cartservice;

        private readonly Services.Interfaces.ICartProductService cartProductservice;

        public UserController(IUserService bl, ICartService cartservice, ICartProductService cartProductservice)
        { 
            service = bl;
            this.cartservice= cartservice;
            this.cartProductservice = cartProductservice;

        }

        // GET: api/<User>
        [HttpGet]
        public IEnumerable<DTO.User> Get()
        {
            IEnumerable <DTO.User> data = service.Get();
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return data;

        }

        // GET api/<User>/5
        [HttpGet("{id}")]
        public DTO.User Get(int id)
        {
          return service.Get(id);
        }

        // GET api/<User>/5
        [HttpPost("signin")]
        public DTO.LoginResponse Singin([FromBody] Login login)
        {
            DTO.User user = service.GetUser(login.email, login.password);
            if (user == null)
            {
                throw new System.Web.Http.HttpResponseException(System.Net.HttpStatusCode.Unauthorized);
            }
            DTO.Cart cart = cartservice.GetByUser(user.Id);
            if (cart == null)
            {
                throw new System.Web.Http.HttpResponseException(System.Net.HttpStatusCode.Unauthorized);
            }
            IEnumerable<DTO.Product> cartProducts=cartProductservice.GetProducts(cart.Id);
            if(cartProducts == null)
            {
                LoginResponse respo = new LoginResponse()
                {
                    User = user,
                    Cart = cart,
                    CartProducts = null,
                };
                return respo;
            }
            LoginResponse response = new LoginResponse()
            {
                User = user,
                Cart = cart,
                CartProducts = cartProducts

            };
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return response;
        }


        // POST api/<User>
        [HttpPost("signup/{password}")]
        //[EnableCors("AllowAllOrigins")]
        public bool SignUp(string password,[FromBody] DTO.User user)
        {
            bool data = service.AddNew(password,user);
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return data;
            
        }
        [HttpPost("updateuser")]
        //[EnableCors("AllowAllOrigins")]
        public bool UpdateUser([FromBody] DTO.User user)
        {
            bool data = service.UpdateUser(user);
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return data;

        }
        
        // DELETE api/<User>/5
        [HttpDelete("{userId}")]
        public void Delete(int userId)
        {
            service.Delete(userId);
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");           
        }
    }
}
