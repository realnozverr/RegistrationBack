using Dapper;
using Npgsql;
using Registration.Web.Models;

namespace Registration.Web.Services
{
    public class Register : IRegister
    {
        private readonly string _connString;
        public Register(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("Connection");
        }
        public async Task<int> Registration(RegisterModel model)
        {
            using (var connection = new NpgsqlConnection(_connString))
            {
                await connection.OpenAsync();

                string sql = @"
            INSERT INTO Register (DateTime, Email, Code)
            VALUES (@DateTime, @Email, @Code);
            ";

                return await connection.ExecuteAsync(sql, new {model.DateTime, model.Email, model.Code});
            }
        }
    }
}
