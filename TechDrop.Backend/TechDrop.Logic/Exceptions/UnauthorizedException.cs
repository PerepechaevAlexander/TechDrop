namespace TechDrop.Logic.Exceptions;

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