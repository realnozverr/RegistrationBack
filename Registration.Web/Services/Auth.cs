using Dapper;
using Npgsql;
using Registration.Web.Models;

namespace Registration.Web.Services
{
    public class Auth : IAuth
    {
        private readonly string _connString;
        
        public Auth(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("Connection");
        }
  
        public async Task<RegisterModel> GetRegister(string email)
        {
            using (var connection = new NpgsqlConnection(_connString))
            {
                await connection.OpenAsync();

                string sql = "SELECT Email, DateTime, Code FROM Register WHERE Email = @Email";

                var result = await connection.QuerySingleOrDefaultAsync<RegisterModel>(sql, new { Email = email });

                return result;
            }
        }
    }
}
