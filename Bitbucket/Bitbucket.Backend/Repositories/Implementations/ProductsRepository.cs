using Bitbucket.Backend.Data;
using Bitbucket.Backend.Helpers;
using Bitbucket.Backend.Repositories.Interfaces;
using Bitbucket.Shared.DTOs;
using Bitbucket.Shared.Entities;
using Bitbucket.Shared.Responses;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Bitbucket.Backend.Repositories.Implementations;

public class ProductsRepository : GenericRepository<Product>, IProductsRepository
{
    private readonly DataContext _context;

    public ProductsRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<ActionResponse<IEnumerable<Product>>> GetAsync(PaginationDTO pagination)
    {
        var queryable = _context.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(pagination.Filter))
        {
            queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
        }

        return new ActionResponse<IEnumerable<Product>>
        {
            WasSuccess = true,
            Result = await queryable
                .OrderBy(x => x.Name)
                .Paginate(pagination)
                .ToListAsync()
        };
    }

    public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
    {
        var queryable = _context.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(pagination.Filter))
        {
            queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
        }

        double count = await queryable.CountAsync();
        int totalPages = (int)Math.Ceiling(count / pagination.RecordsNumber);
        return new ActionResponse<int>
        {
            WasSuccess = true,
            Result = totalPages
        };
    }

    public async Task<ActionResponse<Product>> AddFullAsync(Product product)
    {
        try
        {
            var newProduct = new Product
            {
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
            };

            _context.Add(newProduct);
            await _context.SaveChangesAsync();
            return new ActionResponse<Product>
            {
                WasSuccess = true,
                Result = newProduct
            };
        }
        catch (DbUpdateException)
        {
            return new ActionResponse<Product>
            {
                WasSuccess = false,
                Message = "Ya existe un producto con el mismo nombre."
            };
        }
        catch (Exception exception)
        {
            return new ActionResponse<Product>
            {
                WasSuccess = false,
                Message = exception.Message
            };
        }
    }

    public async Task<ActionResponse<Product>> UpdateFullAsync(Product productDTO)
    {
        try
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == productDTO.Id);
            if (product == null)
            {
                return new ActionResponse<Product>
                {
                    WasSuccess = false,
                    Message = "Producto no existe"
                };
            }

            product.Name = productDTO.Name;
            product.Price = productDTO.Price;
            product.Quantity = productDTO.Quantity;

            _context.Update(product);
            await _context.SaveChangesAsync();
            return new ActionResponse<Product>
            {
                WasSuccess = true,
                Result = product
            };
        }
        catch (DbUpdateException)
        {
            return new ActionResponse<Product>
            {
                WasSuccess = false,
                Message = "Ya existe un producto con el mismo nombre."
            };
        }
        catch (Exception exception)
        {
            return new ActionResponse<Product>
            {
                WasSuccess = false,
                Message = exception.Message
            };
        }
    }
}