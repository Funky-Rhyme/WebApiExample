using Microsoft.AspNetCore.Mvc;
using SmartCleaningAPI.Data;
using SmartCleaningAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SmartCleaningAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ByersContoller : ControllerBase
    {
        private readonly ApiContext _context;

        public ByersContoller(ApiContext context)
        {
            _context = context;
        }

        //Create
        [HttpPost]
        public async Task<JsonResult> Create(Buyer byuer)
        {

            _context.Buyers.Add(byuer);
            await _context.SaveChangesAsync();

            return new JsonResult(Ok(byuer));
        }

        //Update
        [HttpPut("{id}")]
        public async Task<JsonResult> Update(int id, Buyer byuer)
        {
            if  (id != byuer.Id)
            {
                return new JsonResult(BadRequest());
            }

            _context.Entry(byuer).State = EntityState.Modified;

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
            var result = await _context.Buyers.FindAsync(id);

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
            var result = await _context.Buyers.FindAsync(id);

            if (result == null)
            {
                return new JsonResult(NotFound());
            }

            _context.Buyers.Remove(result);
            await _context.SaveChangesAsync();

            return new JsonResult(Ok());
        }
        private bool ItemExist(int id)
        {
            return _context.Buyers.Any(s => s.Id == id);
        }
    }
}
