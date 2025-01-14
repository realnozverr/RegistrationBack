using Registration.Web.Models;

namespace Registration.Web.Services
{
    public interface IAuth
    {
        Task<RegisterModel> GetRegister(string email);
    }
}
