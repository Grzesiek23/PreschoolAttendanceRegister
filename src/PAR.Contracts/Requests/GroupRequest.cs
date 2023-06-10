namespace PAR.Contracts.Requests;

public class GroupRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int TeacherId { get; set; }
    public int SchoolYearId { get; set; }
}