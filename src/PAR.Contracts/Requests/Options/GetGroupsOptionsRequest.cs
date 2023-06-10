namespace PAR.Contracts.Requests.Options;

public class GetGroupsOptionsRequest : PagedRequest
{
    public string? Name { get; init; }
    public int? TeacherId { get; init; }
    public int? SchoolYearId { get; init; }
}