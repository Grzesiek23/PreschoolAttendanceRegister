using PAR.Contracts.Common;

namespace PAR.Contracts.Dtos;

public class PreschoolerDto : BaseDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public int GroupId { get; set; }
    public virtual GroupDto? Group { get; set; }
}