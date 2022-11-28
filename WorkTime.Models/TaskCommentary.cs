using System;
using System.Collections.Generic;

namespace WorkTime.Models;

public partial class TaskCommentary
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string TaskId { get; set; } = null!;

    public string Text { get; set; } = null!;

    public byte[]? AttachedFile { get; set; }

    public virtual WorkTask Task { get; set; } = null!;

    public virtual AspNetUser User { get; set; } = null!;
}
