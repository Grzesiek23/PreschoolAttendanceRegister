namespace PAR.Domain.Common;

public abstract class BaseEntity
{
    public int Id { get; set; } 
    public string? CreatedBy { get; set; } = null!;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public bool IsActive { get; set; }
    public string? InactivatedBy { get; set; }
    public DateTime? InactivatedOn { get; set; }
}