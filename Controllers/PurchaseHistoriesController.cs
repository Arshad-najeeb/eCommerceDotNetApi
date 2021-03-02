using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ECommerceNet.Models;

namespace ECommerceNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseHistoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PurchaseHistoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PurchaseHistories
        [HttpGet]
        public IEnumerable<PurchaseHistory> GetPurchaseHistory()
        {
            return _context.PurchaseHistory;
        }

        // GET: api/PurchaseHistories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPurchaseHistory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var purchaseHistory = await _context.PurchaseHistory.FindAsync(id);

            if (purchaseHistory == null)
            {
                return NotFound();
            }

            return Ok(purchaseHistory);
        }

        // PUT: api/PurchaseHistories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchaseHistory([FromRoute] int id, [FromBody] PurchaseHistory purchaseHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != purchaseHistory.UserId)
            {
                return BadRequest();
            }

            _context.Entry(purchaseHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseHistoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PurchaseHistories
        [HttpPost]
        public async Task<IActionResult> PostPurchaseHistory([FromBody] PurchaseHistory purchaseHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PurchaseHistory.Add(purchaseHistory);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PurchaseHistoryExists(purchaseHistory.UserId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPurchaseHistory", new { id = purchaseHistory.UserId }, purchaseHistory);
        }

        // DELETE: api/PurchaseHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchaseHistory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var purchaseHistory = await _context.PurchaseHistory.FindAsync(id);
            if (purchaseHistory == null)
            {
                return NotFound();
            }

            _context.PurchaseHistory.Remove(purchaseHistory);
            await _context.SaveChangesAsync();

            return Ok(purchaseHistory);
        }

        private bool PurchaseHistoryExists(int id)
        {
            return _context.PurchaseHistory.Any(e => e.UserId == id);
        }
    }
}