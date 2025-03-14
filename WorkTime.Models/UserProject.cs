﻿namespace WorkTime.Models;

public partial class UserProject
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string? ProjectId { get; set; }

    public double HourlyWage { get; set; }

    public string EmpTypeId { get; set; } = null!;

    public double Bonus { get; set; }

    public virtual ICollection<Contract> Contracts { get; } = new List<Contract>();

    public virtual TypeOfEmployment EmpType { get; set; } = null!;

    public virtual Project? Project { get; set; }

    public virtual AspNetUser User { get; set; } = null!;

    public UserProject()
    {
        Id = Guid.NewGuid().ToString();
    }
}
