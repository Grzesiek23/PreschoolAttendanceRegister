namespace PAR.Contracts.Requests;

public class ApplicationUserRequest
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public string Password { get; init; }
    public string ConfirmPassword { get; init; }
    public int Role { get; init; }
    public bool SendEmail { get; init; }
}