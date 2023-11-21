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
        [HttpGet("getall")]
        public IEnumerable<DTO.Product> Get()
        {
            IEnumerable<DTO.Product> data = service.Get();
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return data;
        }

        // GET api/<ProductController>/5
        [HttpGet("getbyid/{id}")]
        public DTO.Product Get(int id)
        {
            return service.Get(id);
        }
        [HttpGet("getavailable")]
        public IEnumerable<DTO.Product> GetAvailable(DateTime from, DateTime to)
        {
            IEnumerable<DTO.Product> data = service.GetAvailable(from, to);
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return data;
        }

        // POST api/<ProductController>
        [HttpPost("addproduct")]
        //[EnableCors("AllowAll")]
        public bool Post(DTO.Product product)
        {
            bool data = service.AddNew(product);
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return data;
        }


        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.Delete(id); 
        }
    }
}
