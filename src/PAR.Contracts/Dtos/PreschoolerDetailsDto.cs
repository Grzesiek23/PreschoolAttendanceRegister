using PAR.Contracts.Common;

namespace PAR.Contracts.Dtos;

public class PreschoolerDetailsDto : BaseDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int GroupId { get; set; }
    public string GroupName { get; set; }
}