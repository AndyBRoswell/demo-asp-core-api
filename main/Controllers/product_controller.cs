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
        public async Task<ActionResult<IEnumerable<product_data_transfer_object>>> get_products() {
            if (_context.products == null) { return NotFound(); }
            return await _context.products.Select(x => to_data_transfer_object(x)).ToListAsync();
        }
        // GET: api/product_controller/5
        [HttpGet("{id}")]
        public async Task<ActionResult<product_data_transfer_object>> get_product(long id) {
            if (_context.products == null) { return NotFound(); }
            var product = await _context.products.FindAsync(id);
            if (product == null) { return NotFound(); }
            return to_data_transfer_object(product);
        }
        // PUT: api/product_controller/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> put_product(long id, product_data_transfer_object product_DTO) {
            if (id != product_DTO.ID) { return BadRequest(); }
            var product = await _context.products.FindAsync(id);
            if (product == null) { return NotFound(); }
            product.brand = product_DTO.brand;
            product.model = product_DTO.model;
            product.batch = product_DTO.batch;
            product.type = product_DTO.type;
            product.intro = product_DTO.intro;
            product.spec = product_DTO.spec;
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
        public async Task<ActionResult<product_data_transfer_object>> post_product(product_data_transfer_object product_DTO) {
            if (_context.products == null) { return Problem("Entity set 'product_database_context.products' is null."); }
            var product = new product {
                ID = product_DTO.ID,
                brand = product_DTO.brand,
                model = product_DTO.model,
                batch = product_DTO.batch,
                type = product_DTO.type,
                intro = product_DTO.intro,
                spec = product_DTO.spec,
            };
            _context.products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(get_product), new { id = product.ID }, to_data_transfer_object(product));
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
        private static product_data_transfer_object to_data_transfer_object(product product) {
            return new product_data_transfer_object {
                ID = product.ID,
                brand = product.brand,
                model = product.model,
                batch = product.batch,
                type = product.type,
                intro = product.intro,
                spec = product.spec,
            };
        }
    }
}
