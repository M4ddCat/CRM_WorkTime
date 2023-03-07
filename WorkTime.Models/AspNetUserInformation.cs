using System;
using System.Collections.Generic;

namespace WorkTime.Models;

public partial class AspNetUserInformation
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string UserTypeId { get; set;} = null!;

    public string BankInformationId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string? Patronymic { get; set; }

    public double HourlyWage { get; set; }

    public byte[]? Photography { get; set; }

    public virtual AspNetUser User { get; set; } = null!;

    public virtual UserType UserType { get; set; } = null!;

    public virtual BankInformation BankInformation { get; set; } = null!;

    public AspNetUserInformation()
    {
        Id = UserId;
    }
}
