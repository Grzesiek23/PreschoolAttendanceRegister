namespace PAR.Application.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message)
    {
    }

    public BadRequestException(string commandName, string message) : base($"[{commandName}] {message}")
    {
    }
}