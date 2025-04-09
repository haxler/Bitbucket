using Bitbucket.Shared.DTOs;
using Bitbucket.Shared.Entities;
using Bitbucket.Shared.Responses;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Bitbucket.Backend.Repositories.Interfaces;

public interface IProductsRepository
{
    Task<ActionResponse<IEnumerable<Product>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);
}