using System;
using System.Collections.Generic;

namespace WorkTime.Models;

public partial class Project
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public double? Bonus { get; set; }

    public virtual ICollection<Invoice> Invoices { get; } = new List<Invoice>();

    public virtual ICollection<UserProject> UserProjects { get; } = new List<UserProject>();

    public virtual ICollection<WorkTask> WorkTasks { get; } = new List<WorkTask>();
}
