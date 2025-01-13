using System.ComponentModel.DataAnnotations;

namespace Registration.Web.Models
{
    public record RegisterModel
    {
        public RegisterModel(string email)
        {
            DateTime = DateTime.UtcNow;
            Email = email;
            Code = Helpers.Helper.GenerateRandomCode();

        }
        public DateTime DateTime { get; init; }

        public string? Email { get; init; }

        public string Code { get; init; }
    }
}
