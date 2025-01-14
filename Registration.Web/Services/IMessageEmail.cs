using Registration.Web.Models;

namespace Registration.Web.Services
{
    public interface IMessageEmail
    {
        Task SendEmail(RegisterModel model);
    }
}
