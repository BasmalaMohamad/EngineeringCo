using Core.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructrue.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            
            if (!context.Documentations.Any())
            {
                var documentationsData = File.ReadAllText("../Infrastructrue/Data/SeedData/Documentations.json");
                var documentations = JsonSerializer.Deserialize<List<Documentation>>(documentationsData);
                context.Documentations.AddRange(documentations);
                await context.SaveChangesAsync();
            }
            if (!context.Products.Any())
            {
                var productsData = File.ReadAllText("../Infrastructrue/Data/SeedData/Products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products == null)
                {

                }
                else
                {
                    context.Products.AddRange(products);
                    await context.SaveChangesAsync();

                }
            }
            if (!context.Categories.Any())
            {
                var categoriesData = File.ReadAllText("../Infrastructrue/Data/SeedData/Categories.json");
                var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData);
                context.Categories.AddRange(categories);
                await context.SaveChangesAsync();
            }
            if (!context.Accessories.Any())
            {
                var accessoriesData = File.ReadAllText("../Infrastructrue/Data/SeedData/Accessories.json");
                var accessories = JsonSerializer.Deserialize<List<Accessories>>(accessoriesData);
                context.Accessories.AddRange(accessories);
                await context.SaveChangesAsync();
            }


        }
    }
}
