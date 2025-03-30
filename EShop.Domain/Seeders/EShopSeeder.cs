using EShop.Domain.Repositories;
using EShopService.Models;

namespace EShop.Domain.Seeders
{
    public static class EShopSeeder
    {
        public static void Seed(DataContext context)
        {
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product { Name = "Product 1", Ean = "1234567890123", Price = 10.99m, Stock = 100, Sku = "SKU001", Category = new Category() {Name = "Electronics" } },
                    new Product { Name = "Product 2", Ean = "1234567890124", Price = 20.99m, Stock = 200, Sku = "SKU002", Category = new Category() { Name = "Books" } },
                    new Product { Name = "Product 3", Ean = "1234567890125", Price = 30.99m, Stock = 300, Sku = "SKU003", Category = new Category() { Name = "Clothing" } }
                );
                context.SaveChanges();
            }
        }
    }
}