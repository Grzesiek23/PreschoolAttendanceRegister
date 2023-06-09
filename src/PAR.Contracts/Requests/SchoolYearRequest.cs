namespace PAR.Contracts.Requests;

public class SchoolYearRequest
{
    public int Id { get; init; }
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
    public bool IsCurrent { get; init; }
}