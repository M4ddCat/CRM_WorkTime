using System;
using System.Collections.Generic;

namespace WorkTime.Models;

public partial class WorkTask
{
    public string Id { get; set; } = null!;

    public string TaskName { get; set; } = null!;

    public string? TaskText { get; set; }

    public string? ProjectId { get; set; }

    public string? PerformerId { get; set; }

    public double CountOfHours { get; set; }

    public int TaskStatusId { get; set; }

    public DateTime? DateOfCompletion { get; set; }

    public string? InvoiceId { get; set; }

    public string IssuerId { get; set; } = null!;

    public virtual AspNetUser Issuer { get; set; } = null!;

    public virtual AspNetUser? Performer { get; set; }

    public virtual Project? Project { get; set; }

    public virtual ICollection<TaskCommentary> TaskCommentaries { get; } = new List<TaskCommentary>();

    public virtual WorkTaskStatus TaskStatus { get; set; } = null!;

    public WorkTask() 
    {
        Id = Guid.NewGuid().ToString();
    }
}
