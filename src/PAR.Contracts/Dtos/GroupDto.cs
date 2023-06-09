using PAR.Contracts.Common;

namespace PAR.Contracts.Dtos;

public class GroupDto : BaseDto
{
    public string Name { get; set; } = null!;
    public string TeacherId { get; set; } = null!;
    public string SchoolYearId { get; set; }
    public virtual SchoolYearDto? SchoolYear { get; set; }
    public virtual UserDto? Teacher { get; set; }
    public virtual ICollection<PreschoolerDto> Preschoolers { get; set; } = new HashSet<PreschoolerDto>();
}