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
    
    public class ProductController : ControllerBase
    {
        private readonly Services.Interfaces.IProductService service;
        public ProductController(IProductService bl)
        {
            service = bl;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public IEnumerable<DTO.Product> Get()
        {
            IEnumerable<DTO.Product> data = service.Get();
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return data;
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public DTO.Product Get(int id)
        {
            return service.Get(id);
        }

        // POST api/<ProductController>
        [HttpPost]
        [EnableCors("AllowAll")]
        public bool Post([FromBody] DTO.Product product)
        {
            bool data = service.AddNew(product);
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return data;
            //    service.AddNew(product);
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
            service.Delete(id); 
        }
    }
}
