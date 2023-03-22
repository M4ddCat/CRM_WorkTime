using System;
using System.Collections.Generic;

namespace WorkTime.Models;

public partial class AspNetUserInformation
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string? Patronymic { get; set; }

    public double HourlyWage { get; set; }

    public byte[]? Photography { get; set; }

    public string? ContactPhone { get; set; }

    public string? ContactEmail { get; set; }

    public string? SocialNetworkContact { get; set; }

    public int? UserTypeId { get; set; }

    public int? LegalUserTypeId { get; set; }

    public string? BankInfoId { get; set; }

    public string? INN { get; set; }

    public string? PassportNum { get; set; }

    public string? PassportGived { get; set; }

    public string? PersonalAddress { get; set; }

    public string? CompanyId { get; set; }

    public virtual BankInformation? BankInfo { get; set; }

    public virtual Company? Company { get; set; }

    public virtual AspNetUser User { get; set; } = null!;

    public virtual UserType? UserType { get; set; }

    public virtual LegalUserType? LegalUserType { get; set; }

    public AspNetUserInformation()
    {
        Id = Guid.NewGuid().ToString();
    }
}
