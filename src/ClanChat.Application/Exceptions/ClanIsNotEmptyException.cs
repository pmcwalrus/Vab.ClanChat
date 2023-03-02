namespace ClanChat.Application.Exceptions;

public class ClanIsNotEmptyException: Exception
{
    public ClanIsNotEmptyException()
    {
    }

    public ClanIsNotEmptyException(string message)
        : base(message)
    {
    }

    public ClanIsNotEmptyException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}