using AcademiaNovit.Models;
using AcademiaNovit.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace AcademiaNovit.Controllers;

[ApiController]
[Route("products")]
public class ProductController : ControllerBase
{
    private readonly AppDbContext _db;

    public ProductController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _db.Products.ToListAsync();
        Log.Information("Read: {@products}", products);
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = await _db.Products.FindAsync(id);
        if (product == null) return NotFound();
        Log.Information("Read: {@product}", product);
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] Product product)
    {
        var validation = ProductValidator.ValidateProduct(product.Name, product.Price);
        if (!validation.IsValid)
        {
            return BadRequest(validation.ErrorMessage);
        }
        Log.Information("Create: {@product}", product);
        _db.Products.Add(product);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product updatedProduct)
    {
        var validation = ProductValidator.ValidateProduct(updatedProduct.Name, updatedProduct.Price);
        if (!validation.IsValid)
        {
            return BadRequest(validation.ErrorMessage);
        }

        var product = await _db.Products.FindAsync(id);
        Log.Information("Update: {@product}", product);
        if (product == null) return NotFound();

        product.Name = updatedProduct.Name;
        product.Price = updatedProduct.Price;
        await _db.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _db.Products.FindAsync(id);
        if (product == null) return NotFound();
        Log.Information("Remove: {@product}", product);
        _db.Products.Remove(product);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}