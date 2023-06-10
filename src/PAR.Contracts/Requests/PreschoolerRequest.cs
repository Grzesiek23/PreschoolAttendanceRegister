namespace PAR.Contracts.Requests;

public class PreschoolerRequest
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public int GroupId { get; set; }
}