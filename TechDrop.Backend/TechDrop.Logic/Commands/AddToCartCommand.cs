using MediatR;
using TechDrop.Data;
using TechDrop.Data.Models;
using TechDrop.Logic.Exceptions;
using TechDrop.Logic.Services;

namespace TechDrop.Logic.Commands;

/// <summary>
/// Добавить товар в корзину
/// </summary>
public class AddToCartCommand : IRequest
{
    /// <summary>
    /// Id товара.
    /// </summary>
    public int ProductId { get; }
    
    /// <summary>
    /// Количество товара для добавления в корзину.
    /// </summary>
    public int Quantity { get; }

    /// <summary>
    /// Создание экземпляра <see cref="AddToCartCommand"/>.
    /// </summary>
    /// <param name="productId">Id товара;</param>
    /// <param name="quantity">кол-во товара, default = 1.</param>
    public AddToCartCommand(int productId, int quantity = 1)
    {
        ProductId = productId;
        Quantity = quantity;
    }
}

public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand>
{
    private readonly UserService _userService;
    private readonly ProductService _productService;
    private readonly CartService _cartService;
    private readonly TechDropDbContext _dbContext;

    
    // TODO Мб сервисы лучше создавать тут в конструкторе, а не инжектить, чтобы не плодить кучу объектов dbContext. Надо протестить.
    public AddToCartCommandHandler(UserService userService, ProductService productService, 
        CartService cartService, TechDropDbContext dbContext)
    {
        _userService = userService;
        _productService = productService;
        _cartService = cartService;
        _dbContext = dbContext;
    }
    
    public async Task Handle(AddToCartCommand request, CancellationToken cancellationToken)
    {
        if (! await _productService.CheckProductById(request.ProductId, cancellationToken))
        {
            throw new NotFoundException("Товар не найден");
        }
        
        // Получаем Id пользователя и запись в корзине
        var userId = await _userService.GetCurrentUserId(cancellationToken);
        var cart = await _cartService.GetCart(request.ProductId, userId, cancellationToken);

        // Если такой товар уже есть в корзине -> прибавляем его кол-во
        if (cart != null)
        {
            cart.Quantity += request.Quantity;
        }
        // Иначе -> добавляем в БД новую запись
        else
        {
            cart = new Cart
            {
                Quantity =request.Quantity,
                UserId = userId,
                ProductId = request.ProductId
            };
            await _dbContext.Carts.AddAsync(cart, cancellationToken);
        }

        // Сохраняем изменения в БД
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}