namespace PAR.Domain.Common;

public abstract class BaseEntity
{
    public int Id { get; set; } 
    public string? CreatedBy { get; set; } = null!;
    public DateTime CreatedOnUtc { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedOnUtc { get; set; }
    public bool IsActive { get; set; }
    public string? InactivatedBy { get; set; }
    public DateTime? InactivatedOnUtc { get; set; }
}