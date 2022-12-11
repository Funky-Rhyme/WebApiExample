using Microsoft.AspNetCore.Mvc;
using SmartCleaningAPI.Data;
using SmartCleaningAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SmartCleaningAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsContoller : ControllerBase
    {
        private readonly ApiContext _context;

        public ProductsContoller(ApiContext context)
        {
            _context = context;
        }

        //Create
        [HttpPost]
        public async Task<JsonResult> Create(Product product)
        {

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return new JsonResult(Ok(product));
        }

        //Update
        [HttpPut("{id}")]
        public async Task<JsonResult> Update(int id, Product product)
        {
            if  (id != product.Id)
            {
                return new JsonResult(BadRequest());
            }

            _context.Entry(product).State = EntityState.Modified;

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
            var result = await _context.Products.FindAsync(id);

            if (result == null)
            {
                return new JsonResult(NotFound());
            }

            return new JsonResult(result);
        }

        //Delete
        [HttpDelete]
        public async Task<JsonResult> Delete(int id)
        {
            var result = await _context.Products.FindAsync(id);

            if (result == null)
            {
                return new JsonResult(NotFound());
            }

            _context.Products.Remove(result);
            await _context.SaveChangesAsync();

            return new JsonResult(Ok());
        }
        private bool ItemExist(int id)
        {
            return _context.Products.Any(s => s.Id == id);
        }
    }
}
