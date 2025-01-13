using Microsoft.AspNetCore.Mvc;
using Registration.Web.Models;
using Registration.Web.Services;
using System.ComponentModel.DataAnnotations;

namespace Registration.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegister _registartionServices;
        private readonly IMessageProducer _messageProducer;
        private readonly IMessageEmail _messageEmail;
        public RegistrationController(IRegister registartionServices, IMessageProducer messageProducer, IMessageEmail messageEmail)
        {
            _registartionServices = registartionServices;
            _messageProducer = messageProducer;
            _messageEmail = messageEmail;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([EmailAddress(ErrorMessage = "Invalid email format.")] string email)
        {
            try
            {
                var model = new RegisterModel(email);
                var result = await _registartionServices.Registration(model);
                if (result >  0)
                {
                    await _messageProducer.SendMessage(model);
                    await _messageEmail.SendEmail(model);
                }
                return Ok(new { message = "Send code." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
