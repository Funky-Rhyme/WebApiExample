using Bogus;
using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;
using SmartCleaningAPI.Models;

namespace SmartCleaningAPI.Data
{
    public class DataGeneration
    {
        public void Generate(ApiContext context)
        {
            GenerateProductData(context);
            GenerateBuyersData(context);
            GenerateSalePointData(context);
            GenerateSaleData(context);
        }

        private void GenerateProductData(ApiContext context)
        {
            int productId = 1;
            var fakeProducts = new Faker<Product>()
                .RuleFor(c => c.Id, b => productId++)
                .RuleFor(c => c.Name, b => b.Commerce.Product())
                .RuleFor(c => c.Price, b => b.Random.Number(0, 100))
                .Generate(15);

            foreach (var temp in fakeProducts)
            {
                context.Products.Add(temp);
            }
            context.SaveChanges();
        }
        private void GenerateSalePointData(ApiContext context)
        {
            int salePointid = 1;
            int productsId = 1;

            var providedProducktsFake = new Faker<ProvidedProduct>()
                .RuleFor(c => c.Id, b => productsId++)
                .RuleFor(c => c.ProductQuantity, b => b.Random.Number(0, 10));


            var fakeSalePoints = new Faker<SalesPoint>()
                .RuleFor(c => c.Id, b => salePointid++)
                .RuleFor(c => c.Name, b => b.Commerce.Department())
                .RuleFor(c => c.ProvidedProducts, b => providedProducktsFake.GenerateBetween(0, 3).ToList())
                .Generate(15);

            foreach (var temp in fakeSalePoints)
            {
                context.SalesPoints.Add(temp);
            }

            context.SaveChanges();
        }
        private void GenerateBuyersData(ApiContext context)
        {
            int buyerId = 1;
            var fakeBuyers = new Faker<Buyer>()
                .RuleFor(c => c.Id, b => buyerId++)
                .RuleFor(c => c.Name, b => b.Person.FullName)
                .RuleFor(c => c.Description, b => b.Lorem.Lines())
                .GenerateBetween(15, 15);

            foreach (var temp in fakeBuyers)
            {
                context.Buyers.Add(temp);
            }
            context.SaveChanges();
        }
        private void GenerateSaleData(ApiContext context)
        {
            int saleId = 1;
            int productId = 1;

            var fakeSaleData = new Faker<SaleData>()
                .RuleFor(c => c.ProductId, b => productId++)
                .RuleFor(c => c.ProductQuantity, b => b.Random.Number(1, 20))
                .RuleFor(c => c.ProductIdAmount, b => b.Random.Number(10, 1000));

            var currentDateTime = DateTime.Now;
            DateOnly currentDateOnly = DateOnly.FromDateTime(currentDateTime);

            var fakeSale = new Faker<Sale>()
                .RuleFor(c => c.Id, b => saleId++)
                .RuleFor(c => c.DateTime, b => b.Date.Past(1, DateTime.Now))
                .RuleFor(c => c.SalesPointId, b => b.Random.Number(1, 5))
                .RuleFor(c => c.BuyerId, b => b.Random.Number(1, 15))
                .RuleFor(c => c.TotalAmount, b => b.Random.Number(0, 10))
                .RuleFor(c => c.SalesData, b => fakeSaleData.GenerateBetween(2, 5))
                .GenerateBetween(2, 6);

            foreach(var temp in fakeSale)
            {
                context.Sales.Add(temp);
            }

            context.SaveChanges();
        }

    }
}
