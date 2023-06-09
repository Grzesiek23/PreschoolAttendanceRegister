﻿using Microsoft.AspNetCore.Identity;

namespace PAR.Domain.Entities
{
    public class ApplicationUserRole : IdentityUserRole<int>
    {
        //public string? Name { get; set; }
        public virtual ApplicationRole ApplicationRole { get; set; } = null!;
        public virtual ApplicationUser User { get; set; } = null!;
    }
}
