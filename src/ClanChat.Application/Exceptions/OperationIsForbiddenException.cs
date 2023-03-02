namespace ClanChat.Application.Exceptions;

public class OperationIsForbiddenException: Exception
{
    public OperationIsForbiddenException()
    {
    }

    public OperationIsForbiddenException(string message)
        : base(message)
    {
    }

    public OperationIsForbiddenException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}