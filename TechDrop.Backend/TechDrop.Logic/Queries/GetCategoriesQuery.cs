using MediatR;
using Microsoft.EntityFrameworkCore;
using TechDrop.Data;
using TechDrop.Logic.Dtos;

namespace TechDrop.Logic.Queries;

/// <summary>
/// Получить все категории товаров
/// </summary>
public class GetCategoriesQuery : IRequest<IList<CategoryDto>> { }

public class GetCategoryQueryHandler : IRequestHandler<GetCategoriesQuery, IList<CategoryDto>>
{
    private readonly TechDropDbContext _dbContext;

    public GetCategoryQueryHandler(TechDropDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IList<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _dbContext.ProductCategories.Select(category => new CategoryDto
        {
            Id = category.ProductCategoryId,
            Name = category.Name,
            Picture = category.Picture
        }).ToListAsync(cancellationToken);
        return categories;
    }
}