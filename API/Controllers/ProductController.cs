using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

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
            return data;
        }

        // POST api/<ProductController>
        [HttpPost("addproduct")]
        public bool Post(DTO.Product product)
        {
            bool data = service.AddNew(product);
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return data;
        }
        [HttpPost("uploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return BadRequest("No image uploaded.");
            }
            var uniqueFileName = image.FileName;

            BlobServiceClient blobServiceClient = new BlobServiceClient("<storage-key>");
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("images");

            BlobClient blobClient = containerClient.GetBlobClient(uniqueFileName);

            var isExist = await blobClient.ExistsAsync();

            if (isExist)
            {
                return BadRequest("כבר קיים קובץ עם שם זהה");
            }
            await blobClient.UploadAsync(image.OpenReadStream(), new BlobHttpHeaders { ContentType = "image/jpeg" });
            return Ok(new { imageName = uniqueFileName });
        }


        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.Delete(id);
        }
    }
}
