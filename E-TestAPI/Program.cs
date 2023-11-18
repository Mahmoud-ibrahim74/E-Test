using E_TestAPI.Context;
using E_TestAPI.Identity;
using E_TestAPI.Repo;
using E_TestAPI.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_TestAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
			builder.Services.AddDbContext<AppDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionDefault")));
            builder.Services.AddCors(CoresService =>
            {
                CoresService.AddPolicy("MyPolicy", coresPolicy =>
                {
                    coresPolicy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
			{
				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
				options.Password.RequiredLength = 6;
				options.Password.RequiredUniqueChars = 0;
			}).AddEntityFrameworkStores<AppDbContext>();
            builder.Services.AddScoped<IAdmin, AdminRepo>();
			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseCors("MyPolicy");   // Cores happen in 2 cases  => 1- diffrenece protocol , 2 - diffrenece domains {configure Cores in services}


            app.MapControllers();

            app.Run();
        }
    }
}