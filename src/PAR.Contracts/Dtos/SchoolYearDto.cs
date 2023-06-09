using PAR.Contracts.Common;

namespace PAR.Contracts.Dtos;

public class SchoolYearDto : BaseDto
{
     public DateOnly StartDate { get; set; }
     public DateOnly EndDate { get; set; }
     public bool IsCurrent { get; set; }
     public virtual ICollection<GroupDto> Groups { get; set; } = new HashSet<GroupDto>();
}