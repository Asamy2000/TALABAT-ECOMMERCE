using E_commerce.Core.Entities;
using E_commerce.Core.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_commerce.Repos.Data
{
    public static class EcommerceContextSeed
    {
        public static async Task SeedAsync(EcommerceContext context)
        {
            //if no data in context.productBrands seed
            if (!context.productBrands.Any())
            {
                //get data from json file
                var brandData = await File.ReadAllTextAsync("../E-commerce.Repos/Data/DataSeeding/brands.json");
                //Serialization process => converting from text to list of ProductBrands
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

                //Adding brands to context
                if (brands is not null && brands.Count > 0)
                {
                    foreach (var brand in brands)
                    {
                        await context.Set<ProductBrand>().AddAsync(brand);

                    }
                    await context.SaveChangesAsync();
                }
            }
            //if no data in context.productTypes seed
            if (!context.productTypes.Any())
            {
                //get data from json file
                var brandTypes = await File.ReadAllTextAsync("../E-commerce.Repos/Data/DataSeeding/types.json");
                //Serialization process => convrting from text to list of ProductBrands
                var types = JsonSerializer.Deserialize<List<ProductType>>(brandTypes);

                //Adding prands to context
                if (types is not null && types.Count > 0)
                {
                    foreach (var type in types)
                    {
                        await context.Set<ProductType>().AddAsync(type);

                    }
                    await context.SaveChangesAsync();
                }
            }
            //if no data in context.products seed
            if (!context.products.Any())
            {
                //get data from json file
                var products = await File.ReadAllTextAsync("../E-commerce.Repos/Data/DataSeeding/products.json");
                //Serialization process => convrting from text to list of ProductBrands
                var productList = JsonSerializer.Deserialize<List<Product>>(products);

                //Adding prands to context
                if (productList is not null && productList.Count > 0)
                {
                    foreach (var product in productList)
                    {
                        await context.Set<Product>().AddAsync(product);

                    }
                    await context.SaveChangesAsync();
                }
            }

            if (!context.DeliveryMethods.Any())
            {
                //get data from json file
                var deliveryMethods = await File.ReadAllTextAsync("../E-commerce.Repos/Data/DataSeeding/delivery.json");
                
                var deliveryMethodList = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethods);

               
                if (deliveryMethodList is not null && deliveryMethodList.Count > 0)
                {
                    foreach (var deliveryMethod in deliveryMethodList)
                    {
                        await context.Set<DeliveryMethod>().AddAsync(deliveryMethod);

                    }
                    await context.SaveChangesAsync();
                }
            }

        }
    }
}
