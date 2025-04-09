using Bitbucket.Backend.Data;
using Bitbucket.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bitbucket.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly DataContext _context;

    public ProductsController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        return Ok(await _context.Products.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(c => c.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(Product product)
    {
        _context.Add(product);
        await _context.SaveChangesAsync();
        return Ok(product);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(c => c.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        _context.Remove(product);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> PutAsync(Product product)
    {
        _context.Update(product);
        await _context.SaveChangesAsync();
        return Ok(product);
    }
}