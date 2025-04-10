using Bitbucket.Shared.DTOs;
using Bitbucket.Shared.Entities;
using Bitbucket.Shared.Responses;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Bitbucket.Backend.UnitsOfWork.Interfaces;

public interface IProductsUnitOfWork
{
    Task<ActionResponse<IEnumerable<Product>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

    Task<ActionResponse<Product>> AddFullAsync(Product product);

    Task<ActionResponse<Product>> UpdateFullAsync(Product product);
}