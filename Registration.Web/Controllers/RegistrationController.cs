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
        private readonly IRegister _registartion;
        private readonly IMessageProducer _messageProducer;
        private readonly IMessageEmail _messageEmail;
        public RegistrationController(IRegister registartionServices, IMessageProducer messageProducer, IMessageEmail messageEmail)
        {
            _registartion = registartionServices;
            _messageProducer = messageProducer;
            _messageEmail = messageEmail;
        }

        [HttpPost]
        public async Task<ActionResult> Register([EmailAddress(ErrorMessage = "Invalid email format.")] string email)
        {
            try
            {
                var options = new CookieOptions
                {
                    Path = "/",
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTimeOffset.UtcNow.AddDays(1)
                };
                var model = new RegisterModel(email);
                var result = await _registartion.Registration(model);
                if (result >  0)
                {
                    Response.Cookies.Append("regData", $"{model.Email}|false", options);
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
