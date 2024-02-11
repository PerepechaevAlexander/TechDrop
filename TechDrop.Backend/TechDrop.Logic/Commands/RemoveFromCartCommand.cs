using MediatR;
using TechDrop.Data;
using TechDrop.Logic.Exceptions;
using TechDrop.Logic.Services;

namespace TechDrop.Logic.Commands;

/// <summary>
/// Удалить товар из корзины.
/// </summary>
public class RemoveFromCartCommand : IRequest
{
    /// <summary>
    /// Id товара.
    /// </summary>
    public int ProductId { get; }
    
    /// <summary>
    /// Количество товара для удаления.
    /// </summary>
    public int Quantity { get; }

    /// <summary>
    /// Создание экземпляра <see cref="RemoveFromCartCommand"/>.
    /// </summary>
    /// <param name="productId">Id товара;</param>
    /// <param name="quantity">кол-во товара, default = 1.</param>
    public RemoveFromCartCommand(int productId, int quantity = 1)
    {
        ProductId = productId;
        Quantity = quantity;
    }
}

public class RemoveFromCartCommandHandler : IRequestHandler<RemoveFromCartCommand>
{
    private readonly UserService _userService;
    private readonly ProductService _productService;
    private readonly CartService _cartService;
    private readonly TechDropDbContext _dbContext;

    public RemoveFromCartCommandHandler(UserService userService, ProductService productService, 
        CartService cartService, TechDropDbContext dbContext)
    {
        _userService = userService;
        _productService = productService;
        _cartService = cartService;
        _dbContext = dbContext;
    }

    public async Task Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
    {
        if (! await _productService.CheckProductById(request.ProductId, cancellationToken))
        {
            throw new NotFoundException("Товар не найден.");
        }
        
        // Получаем Id пользователя и запись в корзине
        var userId = await _userService.GetCurrentUserId(cancellationToken);
        var cart = await _cartService.GetCart(request.ProductId, userId, cancellationToken);

        // Если запись в корзине не найдена -> кидаем ошибку 404
        if (cart == null)
        {
            throw new NotFoundException("Товар отсутствует в корзине.");
        }
        
        // Уменьшаем кол-во товара в корзине
        cart.Quantity -= request.Quantity;

        // Если товара в корзине не осталось -> удаляем запись
        if (cart.Quantity <= 0)
        {
            _dbContext.Carts.Remove(cart);
        }

        // Сохраняем изменения в БД
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}