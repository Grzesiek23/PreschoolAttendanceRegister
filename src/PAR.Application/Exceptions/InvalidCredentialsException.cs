namespace PAR.Application.Exceptions;

public class InvalidCredentialsException : Exception
{
    public InvalidCredentialsException() : base("Invalid credentials")
    {
    }
}