using Bitbucket.Backend.Repositories.Interfaces;
using Bitbucket.Backend.UnitsOfWork.Interfaces;
using Bitbucket.Shared.DTOs;
using Bitbucket.Shared.Entities;
using Bitbucket.Shared.Responses;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Bitbucket.Backend.UnitsOfWork.Implementations;

public class ProductsUnitOfWork : GenericUnitOfWork<Product>, IProductsUnitOfWork
{
    private readonly IProductsRepository _productsRepository;

    public ProductsUnitOfWork(IGenericRepository<Product> repository, IProductsRepository productsRepository) : base(repository)
    {
        _productsRepository = productsRepository;
    }

    public override async Task<ActionResponse<IEnumerable<Product>>> GetAsync(PaginationDTO pagination) => await _productsRepository.GetAsync(pagination);

    public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _productsRepository.GetTotalPagesAsync(pagination);

    public async Task<ActionResponse<Product>> AddFullAsync(Product product) => await _productsRepository.AddFullAsync(product);

    public async Task<ActionResponse<Product>> UpdateFullAsync(Product product) => await _productsRepository.UpdateFullAsync(product);
}