using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly Services.Interfaces.ICategoryService service;
        public CategoryController(ICategoryService bl)
        {
            service = bl;
        }
        // GET: api/<CategoryController>
        [HttpGet("Get")]
        public IEnumerable<DTO.Category> Get()
        {
            return service.Get();   
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public DTO.Category Get(int id)
        {
            return service.Get(id);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public void Post([FromBody] DTO.Category category)
        {
            service.AddNew(category);
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] DTO.Category category)
        {
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
