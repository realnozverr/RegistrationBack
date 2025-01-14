
using Registration.Web.Middleware;
using Registration.Web.Services;

namespace Registration.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<IRegister, Register>();
            builder.Services.AddSingleton<IAuth, Auth>();
            builder.Services.AddTransient<IMessageProducer, MessageProducer>();
            builder.Services.AddScoped<IMessageEmail,  MessageEmail>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<CookieMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
