namespace PAR.Contracts.Requests;

public class SchoolYearRequest
{
    public int Id { get; init; }
    public string Name { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public bool IsCurrent { get; init; }
}