using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System.IO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly Services.Interfaces.IProductService service;
        private readonly IConfiguration configuration;
        public ProductController(IProductService bl, IConfiguration configuration)
        {
            service = bl;
            this.configuration = configuration;
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

            var uniqueFileName = $"{Guid.NewGuid():N}_{Path.GetFileName(image.FileName)}";

            // Read connection string from configuration
            var connectionString = configuration["AzureStorage:ConnectionString"];
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                return StatusCode(500, "Storage connection string is not configured.");
            }

            try
            {
                var blobServiceClient = new BlobServiceClient(connectionString);
                var containerClient = blobServiceClient.GetBlobContainerClient("images");

                await containerClient.CreateIfNotExistsAsync();

                var blobClient = containerClient.GetBlobClient(uniqueFileName);

                if (await blobClient.ExistsAsync())
                {
                    return BadRequest("כבר קיים קובץ עם שם זהה");
                }

                var contentType = string.IsNullOrWhiteSpace(image.ContentType) ? "application/octet-stream" : image.ContentType;

                await blobClient.UploadAsync(image.OpenReadStream(), new BlobHttpHeaders { ContentType = contentType });
                return Ok(new { imageName = uniqueFileName });
            }
            catch (Azure.RequestFailedException ex)
            {
                return StatusCode(502, $"Storage request failed: {ex.Message}");
            }
            catch (FormatException)
            {
                return StatusCode(500, "Storage connection string is malformed. Check configuration.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Unexpected error: {ex.Message}");
            }
        }


        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.Delete(id);
        }
    }
}
