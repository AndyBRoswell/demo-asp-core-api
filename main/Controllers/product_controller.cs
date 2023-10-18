using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using main.Models;

namespace main.Controllers {
    //[Route("api/[controller]")]
    [Route("api/product")]
    [ApiController]
    public class product_controller : ControllerBase {
        private readonly product_database_context _context;
        public product_controller(product_database_context context) { _context = context; }
        // GET: api/product_controller
        [HttpGet]
        public async Task<ActionResult<IEnumerable<product>>> get_products() {
            if (_context.products == null) { return NotFound(); }
            return await _context.products.ToListAsync();
        }
        // GET: api/product_controller/5
        [HttpGet("{id}")]
        public async Task<ActionResult<product>> get_product(long id) {
            if (_context.products == null) { return NotFound(); }
            var product = await _context.products.FindAsync(id);
            if (product == null) { return NotFound(); }
            return product;
        }
        // PUT: api/product_controller/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> put_product(long id, product product) {
            if (id != product.ID) { return BadRequest(); }
            _context.Entry(product).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException) {
                if (!product_exists(id)) { return NotFound(); }
                else { throw; }
            }
            return NoContent();
        }
        // POST: api/product_controller
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<product>> post_product(product product) {
            if (_context.products == null) { return Problem("Entity set 'product_database_context.products' is null."); }
            _context.products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(get_product), new { id = product.ID }, product);
        }
        // DELETE: api/product_controller/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> delete_product(long id) {
            if (_context.products == null) { return NotFound(); }
            var product = await _context.products.FindAsync(id);
            if (product == null) { return NotFound(); }
            _context.products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool product_exists(long id) { return (_context.products?.Any(e => e.ID == id)).GetValueOrDefault(); }
    }
}
