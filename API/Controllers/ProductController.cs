using Microsoft.AspNetCore.Mvc;
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

    public class ProductController : ControllerBase
    {
        private readonly Services.Interfaces.IProductService service;
        public ProductController(IProductService bl)
        {
            service = bl;
        }
        // Example API endpoint to handle image upload

        // GET: api/<ProductController>
        [HttpGet("getall")]
       
        public IEnumerable<DTO.Product> Get()
        {
            IEnumerable<DTO.Product> data = service.Get();
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return data;
        }
        [HttpGet("getimages")]
        public IEnumerable<string> GetImages()
        {
            IEnumerable<string> data = service.GetImages();
            return data;
        }
        // GET api/<ProductController>/5
        [HttpGet("getbyid/{id}")]
        public DTO.Product Get(int id)
        {
            return service.Get(id);
        }
        [HttpGet("getavailable/{from}/{to}")]
        [Authorize]
        public List<DTO.Product> GetAvailable(DateTime from, DateTime to)
        {
            List<DTO.Product> data = service.GetAvailable(from, to);
          //  HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
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
        [HttpPost("uploadImage")]
        public IActionResult UploadImage(IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return BadRequest("No image uploaded.");
            }

            // Save the image to a folder in your server
            //var uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
            var uniqueFileName =  image.FileName;
            
            var filePath = Path.Combine("C:\\Users\\User\\Documents\\development\\MoreshetProject\\API\\Static\\", uniqueFileName);
            if (System.IO.File.Exists(filePath))
            {
                return BadRequest("כבר קיים קובץ עם שם זהה");
            }
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            // You can save the image path or uniqueFileName in the database
            // In this case, let's return the uniqueFileName to the frontend
            return Ok(new { imageName = uniqueFileName }); // Return the image filename to the frontend
        }


        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.Delete(id); 
        }
    }
}
