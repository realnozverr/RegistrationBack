using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Registration.Web.Models;
using Registration.Web.Services;

namespace Registration.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _auth;
        public AuthController(IAuth auth)
        {
            _auth = auth;
        }
        [HttpPost]
        public async Task<ActionResult> Auth([FromBody] string code)
        {
            try
            {
                if (string.IsNullOrEmpty(code))
                {
                    return BadRequest(new { message = "Code cannot be empty." });
                }

                if (Request.Cookies.TryGetValue("regData", out var cookieValue))
                {
                    var parts = cookieValue.Split('|');

                    if (parts.Length == 2)
                    {
                        var email = parts[0];
                        var someFlag = bool.Parse(parts[1]);

                        var result = await _auth.GetRegister(email);

                        // Проверка на null
                        if (result == null)
                        {
                            return NotFound(new { message = "Register model not found." });
                        }

                        if (result.Code == code)
                        {
                            var newCookieValue = $"{email}|true";
                            var options = new CookieOptions
                            {
                                Path = "/",
                                HttpOnly = true,
                                Secure = true,
                                Expires = DateTimeOffset.UtcNow.AddDays(1)
                            };

                            Response.Cookies.Append("regData", newCookieValue, options);

                            return Ok(new { message = "You accepted the code." });
                        }
                        else
                        {
                            return Unauthorized(new { message = "Invalid code." });
                        }
                    }
                    else
                    {
                        return BadRequest(new { message = "Invalid cookie format." });
                    }
                }
                else
                {
                    return NotFound(new { message = "Cookie not found." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
