namespace PAR.Application.Exceptions;

public class NotFoundException : Exception
{
    public string CommandName { get; set; }

    public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) was not found.")
    {
    }

    public NotFoundException(string commandName, string name, object key) : base(
        $"[{commandName}] Entity \"{name}\" ({key}) was not found.")
    {
        CommandName = name;
    }
}