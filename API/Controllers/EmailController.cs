using DTO;
using Microsoft.AspNetCore.Mvc;
using Services;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly Services.Interfaces.IEmailService _emailService;

        public EmailController(Services.Interfaces.IEmailService emailService)
        {
            _emailService = emailService;
        }

        // Your API endpoint for handling the contact form submission
        [HttpPost("submitcontactform")]
        public bool SubmitContactForm([FromBody] ContactDetails details)
        {
           if( _emailService.SendContactFormEmail(details.Name, details.Email,details.Phone, details.Message))
            return true;
           return false;
        }
        [HttpPost("confirmorder")]
        [Authorize]

        public bool ConfirmOreder([FromBody] DTO.OrderData data)
        {
            if (_emailService.SendConfirmOrderToClient(data.order, data.user))
                return true;
            return false;
        }

    }
}
