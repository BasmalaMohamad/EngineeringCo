using Core.Interfaces;
using Infrastructrue.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
      
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

<<<<<<< Updated upstream
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
            builder.Services.AddDbContext<StoreContext>(opt =>
            {
                opt.UseSqlServer(connectionString);
            }, ServiceLifetime.Scoped);

            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
=======
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

// Apply DI
builder.Services.AddScoped<IProductRepository, ProductRepository>();
            
// Adding AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IProductRepository, ProductRepository>();
var app = builder.Build();
using var scope = app.Services.CreateScope();
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
>>>>>>> Stashed changes

app.UseAuthorization();


app.MapControllers();

<<<<<<< Updated upstream
            app.Run();
        }
    }
}
=======
app.UseStaticFiles();

app.Run();
 
>>>>>>> Stashed changes
