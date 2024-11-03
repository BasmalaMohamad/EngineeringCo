
using Core.Interfaces;
using Infrastructrue.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API
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
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Specify DBContext with Connection String
            builder.Services.AddDbContext<StoreContext>(opt =>
            {
                opt.UseSqlServer(connectionString);
            }, ServiceLifetime.Scoped);

            // Apply DI
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            
            // Adding AutoMapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.UseStaticFiles();

            app.Run();
        }
    }
}
