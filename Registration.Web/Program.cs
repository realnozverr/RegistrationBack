
<<<<<<< HEAD
using Registration.Web.Middleware;
=======
>>>>>>> 920698c766560959fc8825e98a787fe98efc5770
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
<<<<<<< HEAD
            builder.Services.AddSingleton<IAuth, Auth>();
=======
>>>>>>> 920698c766560959fc8825e98a787fe98efc5770
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

<<<<<<< HEAD
            app.UseMiddleware<CookieMiddleware>();

=======
>>>>>>> 920698c766560959fc8825e98a787fe98efc5770
            app.MapControllers();

            app.Run();
        }
    }
}
