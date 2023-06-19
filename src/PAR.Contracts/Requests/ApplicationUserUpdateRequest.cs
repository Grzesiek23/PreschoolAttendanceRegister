namespace PAR.Contracts.Requests;

public class ApplicationUserUpdateRequest
{
    public int Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string? PhoneNumber { get; init; }
    public int RoleId { get; init; }
}