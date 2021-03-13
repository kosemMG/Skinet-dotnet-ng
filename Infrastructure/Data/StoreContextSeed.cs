using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
  public class StoreContextSeed
  {
    public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
    {
      try
      {
        if (!context.ProductBrands.Any())
        {
          var brandsJson = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/brands.json");
          var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsJson);

          if (brands != null)
          {
            foreach (var brand in brands)
              await context.ProductBrands.AddAsync(brand);
            
            await context.SaveChangesAsync();
          }
        }
        
        if (!context.ProductTypes.Any())
        {
          var typesJson = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/types.json");
          var types = JsonSerializer.Deserialize<List<ProductType>>(typesJson);

          if (types != null)
          {
            foreach (var type in types)
              await context.ProductTypes.AddAsync(type);

            await context.SaveChangesAsync(); 
          }
        }
        
        if (!context.Products.Any())
        {
          var productsJson = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");
          var products = JsonSerializer.Deserialize<List<Product>>(productsJson);

          if (products != null)
          {
            foreach (var product in products)
              await context.Products.AddAsync(product);

            await context.SaveChangesAsync(); 
          }
        }
      }
      catch (Exception e)
      {
        var logger = loggerFactory.CreateLogger<StoreContextSeed>();
        logger.LogError(e.Message);
      }
    }
  }
}