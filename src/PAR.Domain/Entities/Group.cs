﻿using PAR.Domain.Common;

namespace PAR.Domain.Entities;

public class Group : BaseEntity
{
    public string Name { get; set; } = null!;
    public int TeacherId { get; set; }
    public int SchoolYearId { get; set; }
    public virtual SchoolYear? SchoolYear { get; set; }
    public virtual ApplicationUser? Teacher { get; set; }
    public virtual ICollection<Preschooler> Preschoolers { get; set; } = new HashSet<Preschooler>();
}