using Bitbucket.Backend.UnitsOfWork.Interfaces;
using Bitbucket.Shared.DTOs;
using Bitbucket.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Bitbucket.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : GenericController<Product>
{
    private readonly IProductsUnitOfWork _productsUnitOfWork;

    public ProductsController(IGenericUnitOfWork<Product> unit, IProductsUnitOfWork productsUnitOfWork) : base(unit)
    {
        _productsUnitOfWork = productsUnitOfWork;
    }

    [HttpGet]
    public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
    {
        var response = await _productsUnitOfWork.GetAsync(pagination);
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return BadRequest();
    }

    [HttpGet("totalPages")]
    public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
    {
        var action = await _productsUnitOfWork.GetTotalPagesAsync(pagination);
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return BadRequest();
    }
}