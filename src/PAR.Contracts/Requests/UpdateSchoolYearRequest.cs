namespace PAR.Contracts.Requests;

public class UpdateSchoolYearRequest
{
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
    public bool IsCurrent { get; init; }
}