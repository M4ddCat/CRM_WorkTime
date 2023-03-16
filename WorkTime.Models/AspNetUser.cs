using System;
using System.Collections.Generic;

namespace WorkTime.Models;

public partial class AspNetUser
{
    public string Id { get; set; } = null!;

    public string? UserName { get; set; }

    public string? NormalizedUserName { get; set; }

    public string? Email { get; set; }

    public string? NormalizedEmail { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; } = new List<AspNetUserClaim>();

    public virtual ICollection<AspNetUserInformation> AspNetUserInformations { get; } = new List<AspNetUserInformation>();

    public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; } = new List<AspNetUserLogin>();

    public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; } = new List<AspNetUserToken>();

    public virtual ICollection<Contract> ContractCustomerPeople { get; } = new List<Contract>();

    public virtual ICollection<Project> ProjectCustomerPeople { get; } = new List<Project>();

    public virtual ICollection<Contract> ContractPerformerPeople { get; } = new List<Contract>();

    public virtual ICollection<Invoice> Invoices { get; } = new List<Invoice>();

    public virtual ICollection<TaskCommentary> TaskCommentaries { get; } = new List<TaskCommentary>();

    public virtual ICollection<UserProject> UserProjects { get; } = new List<UserProject>();

    public virtual ICollection<WorkTask> WorkTaskIssuers { get; } = new List<WorkTask>();

    public virtual ICollection<WorkTask> WorkTaskPerformers { get; } = new List<WorkTask>();

    public virtual ICollection<AspNetRole> Roles { get; } = new List<AspNetRole>();

    public AspNetUser()
    {
        Id = Guid.NewGuid().ToString();
    }
}
