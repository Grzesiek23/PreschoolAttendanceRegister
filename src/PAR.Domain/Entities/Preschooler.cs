using PAR.Domain.Common;

namespace PAR.Domain.Entities;

public class Preschooler : BaseEntity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public int GroupId { get; set; }
    public virtual Group Group { get; set; }
}