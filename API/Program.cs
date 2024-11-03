
using Core.Interfaces;
using Infrastructrue.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;


       
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
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

<<<<<<< HEAD
            // Apply DI
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            
            // Adding AutoMapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
=======
builder.Services.AddScoped<IProductRepository, ProductRepository>();
var app = builder.Build();
using var scope = app.Services.CreateScope();
>>>>>>> 96a022a0f166661176d807174d03e625b369fba7

var services = scope.ServiceProvider;
var context = services.GetRequiredService<StoreContext>();
var logger = services.GetRequiredService<ILogger<Program>>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            try
            {

                await StoreContextSeed.SeedAsync(context);
                
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
            }

            app.UseAuthorization();


            app.MapControllers();

            app.UseStaticFiles();

            app.Run();
 
