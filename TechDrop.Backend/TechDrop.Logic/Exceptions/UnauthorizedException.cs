namespace TechDrop.Logic.Exceptions;

/// <summary>
/// Исключение, выбрасываемое при отсутствии прав доступа у пользователя (если он неавторизован)
/// </summary>
public class UnauthorizedException : Exception
{
    public int Code { get; }
    public override string Message { get; }

    public UnauthorizedException(string message)
    {
        Code = 401;
        Message = message;
    }
}