using PAR.Domain.Common;

namespace PAR.Domain.Entities;

public class SchoolYear : BaseEntity
{
     public DateOnly StartDate { get; set; }
     public DateOnly EndDate { get; set; }
}