using Microsoft.AspNetCore.Mvc;
using SmartCleaningAPI.Data;
using SmartCleaningAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SmartCleaningAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesPointController : ControllerBase
    {
        private readonly ApiContext _context;

        public SalesPointController(ApiContext context)
        {
            _context = context;
        }

        //Create
        [HttpPost]
        public async Task<JsonResult> Create(SalesPoint salePoint)
        {

            _context.SalesPoints.Add(salePoint);
            await _context.SaveChangesAsync();

            return new JsonResult(Ok(salePoint));
        }

        //Update
        [HttpPut("{id}")]
        public async Task<JsonResult> Update(int id, SalesPoint salePoint)
        {
            if  (id != salePoint.Id)
            {
                return new JsonResult(BadRequest());
            }

            _context.Entry(salePoint).State = EntityState.Modified;

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
            var result = await _context.SalesPoints.FindAsync(id);

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
            var result = await _context.SalesPoints.FindAsync(id);

            if (result == null)
            {
                return new JsonResult(NotFound());
            }

            _context.SalesPoints.Remove(result);
            await _context.SaveChangesAsync();

            return new JsonResult(Ok());
        }
        private bool ItemExist(int id)
        {
            return _context.SalesPoints.Any(s => s.Id == id);
        }
    }
}
