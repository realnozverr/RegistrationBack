using Registration.Web.Models;

namespace Registration.Web.Services
{
    public interface IRegister
    {
        Task<int> Registration(RegisterModel model);
    }
}
