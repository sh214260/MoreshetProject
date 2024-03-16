using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using DTO;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly Services.Interfaces.IOrderService service;
        //private readonly Services.Interfaces.IEmailService _emailService;
        public OrderController(IOrderService bl)
        {
            service = bl;
        }
        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<DTO.Order> Get()
        {
             return service.Get();
        }
        // GET: api/<OrderController>
        [HttpGet("getbyuser/{userId}")]
        public IEnumerable<DTO.Order> GetByUser(int userId)
        {
            return service.GetByUser(userId);
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public DTO.Order Get(int id)
        {
            return service.Get(id);             
        }
        [HttpGet("getAllData/{orderId}")]
        public DTO.OrderData GetAllData(int orderId)
        {
            return service.GetAllData(orderId);
        }

        // GET api/<OrderController>/5
        [HttpGet("getByDate/{date}")]
        public IEnumerable<OrderByDay> GetByDate( string date)
        {
            DateOnly d1 = DateOnly.Parse(date);
            return service.GetByDate(d1);
        }

        // POST api/<OrderController>
        [HttpPost("addorder")]
        [Authorize]
        public int Post([FromBody] DTO.Order order)
        {
             return service.AddNew(order);
        }
        [HttpGet("getdeliveryprice/{cartId}")]
        [Authorize]
        public int GetDeliveryPrice(int cartId)
        {
            return service.GetDeliveryPrice(cartId);
        }

        
    }
}
