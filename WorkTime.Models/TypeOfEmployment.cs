using System;
using System.Collections.Generic;

namespace WorkTime.Models;

public partial class TypeOfEmployment
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int Tax { get; set; }

    public virtual ICollection<UserProject> UserProjects { get; } = new List<UserProject>();
}
