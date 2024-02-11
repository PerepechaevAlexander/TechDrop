using MediatR;
using Microsoft.EntityFrameworkCore;
using TechDrop.Data;
using TechDrop.Logic.Dto;
using TechDrop.Logic.Services;

namespace TechDrop.Logic.Queries;

/// <summary>
/// Получить кол-во каждого товара в корзине.
/// </summary>
public class GetQuantityOfProductsInCartQuery : IRequest<IList<ProductQuantityDto>> { }

public class GetQuantityOfProductsInCartQueryHandler
    : IRequestHandler<GetQuantityOfProductsInCartQuery, IList<ProductQuantityDto>>
{
    private readonly UserService _userService;
    private readonly TechDropDbContext _dbContext;

    public GetQuantityOfProductsInCartQueryHandler(UserService userService,TechDropDbContext dbContext)
    {
        _userService = userService;
        _dbContext = dbContext;
    }
    
    public async Task<IList<ProductQuantityDto>> Handle(GetQuantityOfProductsInCartQuery request, CancellationToken cancellationToken)
    {
        var userId = await _userService.GetCurrentUserId(cancellationToken);

        return await _dbContext.Carts
            .Where(c => c.UserId == userId)
            .Select(c => new ProductQuantityDto
            {
                ProductId = c.ProductId,
                Quantity = c.Quantity
            })
            .ToListAsync(cancellationToken);
    }
}