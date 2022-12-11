using Microsoft.AspNetCore.Mvc;
using SmartCleaningAPI.Data;
using SmartCleaningAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SmartCleaningAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ApiContext _context;

        public SalesController(ApiContext context)
        {
            _context = context;
        }

        //Create
        [HttpPost]
        public async Task<JsonResult> Create(Sale sale)
        {

            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            return new JsonResult(Ok(sale));
        }

        //Update
        [HttpPut("{id}")]
        public async Task<JsonResult> Update(int id, Sale sale)
        {
            if (id != sale.Id)
            {
                return new JsonResult(BadRequest());
            }

            _context.Entry(sale).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExist(id))
                {
                    return new JsonResult(NotFound());
                }
                else
                {
                    throw;
                }
            }

            return new JsonResult(NoContent());
        }


        //Read
        [HttpGet("{id}")]
        public async Task<JsonResult> Get(int id)
        {
            var result = await _context.Sales.FindAsync(id);

            if (result == null)
            {
                return new JsonResult(NotFound());
            }

            return new JsonResult(result);
        }

        //Delete
        [HttpDelete("{id}")]
        public async Task<JsonResult> Delete(int id)
        {
            var result = await _context.Sales.FindAsync(id);

            if (result == null)
            {
                return new JsonResult(NotFound());
            }

            _context.Sales.Remove(result);
            await _context.SaveChangesAsync();

            return new JsonResult(Ok());
        }
        private bool ItemExist(int id)
        {
            return _context.Sales.Any(s => s.Id == id);
        }

        [HttpPost("{buyerId},{salePointId},{productId},{quantity}")]
        public async Task<JsonResult> DoSale(int buyerId = 0, int salePointId = 0, int productId = 0, int quantity = 0)
        {
            var salePoint = await _context.SalesPoints.FirstOrDefaultAsync(u => u.Id == salePointId);

            if (salePoint == null)
            {
                return new JsonResult(NotFound());
            }

            var providedProduct = salePoint.ProvidedProducts.FirstOrDefault(u => u.Id == productId);

            if (providedProduct == null) 
            {
                return new JsonResult(NotFound());
            }

            var product = await _context.Products.FindAsync(productId);

            providedProduct.ProductQuantity -= quantity;
            _context.Sales.Add(new Sale
            {
                BuyerId = buyerId,
                DateTime = DateTime.UtcNow,
                SalesPointId = salePointId,
                TotalAmount = product.Price * quantity,
            });

            if(buyerId > 0)
            {
                var buyer = _context.Buyers.Find(buyerId);
                
                if(buyer != null)
                {
                    buyer.SalesIds.Add(new SalesIds
                    {
                        SalesId = _context.Sales.FirstOrDefaultAsync(_ => _.BuyerId == buyerId).Id,
                    });
                }
            }
            return new JsonResult(Ok());
        }
    }
}
