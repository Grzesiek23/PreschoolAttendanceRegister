namespace PAR.Contracts.Requests.Options;

public class GetSchoolYearsOptionsRequest : PagedRequest
{
    public string? Name { get; init; }
    public DateOnly? StartDate { get; init; }
    public DateOnly? EndDate { get; init; }
    public bool? IsCurrent { get; init; }
}