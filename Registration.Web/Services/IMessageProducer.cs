using Registration.Web.Models;

namespace Registration.Web.Services
{
    public interface IMessageProducer
    {
        Task SendMessage(RegisterModel model);
    }
}
