﻿namespace TechDrop.Logic.Exceptions;

/// <summary>
/// Исключение, выбрасываемое при возникновении ошибки на стороне сервера
/// </summary>
public class InternalServerException : Exception
{
    public int Code { get; }
    public override string Message { get; }
    
    public InternalServerException()
    {
        Code = 500;
        Message = "Ошибка на стороне сервера..";
    }

    public InternalServerException(string message)
    {
        Code = 500;
        Message = message;
    }
}