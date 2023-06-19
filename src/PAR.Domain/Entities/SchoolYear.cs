using PAR.Domain.Common;

namespace PAR.Domain.Entities;

public class SchoolYear : BaseEntity
{
     public string Name { get; set; } = null!;
     public DateOnly StartDate { get; set; }
     public DateOnly EndDate { get; set; }
     public bool IsCurrent { get; set; }
     public virtual ICollection<Group> Groups { get; set; } = new HashSet<Group>();
}