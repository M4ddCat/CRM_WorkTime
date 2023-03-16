using System;
using System.Collections.Generic;

namespace WorkTime.Models;

public partial class Project
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public double? Bonus { get; set; }

    public string? CustomerPersonId { get; set; }

    public string? CustomerCompanyId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual Company? CustomerCompany { get; set; }

    public virtual AspNetUser? CustomerPerson { get; set; }

    public virtual ICollection<Invoice> Invoices { get; } = new List<Invoice>();

    public virtual ICollection<UserProject> UserProjects { get; } = new List<UserProject>();

    public virtual ICollection<WorkTask> WorkTasks { get; } = new List<WorkTask>();

    public Project()
    {
        Id = Guid.NewGuid().ToString();
    }

}
