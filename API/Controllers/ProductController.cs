using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using DTO;
using Repositories.Models;
using Services;

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
            return service.Get();
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        //public DTO.Product Get(int id)
        //{
        //   // return "value";
        //}

        // POST api/<ProductController>
        [HttpPost]
        public void Post([FromBody] DTO.Product product)
        {
            service.AddNew(product);
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
        }
    }
}
