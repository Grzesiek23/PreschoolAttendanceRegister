namespace PAR.Contracts.Requests.Options;

public class GetUsersOptionsRequest : PagedRequest
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Email { get; init; }
}