namespace PAR.Application.Exceptions;

public class InternalApplicationError : Exception
{
    public InternalApplicationError(string message) : base(message)
    {
    }

    public InternalApplicationError(string commandName, string message) : base($"[{commandName}] {message}")
    {
    }
}