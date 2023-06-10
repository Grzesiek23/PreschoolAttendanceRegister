namespace PAR.Contracts.Requests.Options;

public class GetPreschoolersOptionsRequest : PagedRequest
{
    public string? FirstName { get; init; } 
    public string? LastName { get; init; }
    public int? GroupId { get; init; }
}