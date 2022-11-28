using System;
using System.Collections.Generic;

namespace WorkTime.Models;

public partial class WorkTaskStatus
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<WorkTask> WorkTasks { get; } = new List<WorkTask>();
}
