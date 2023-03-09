using System;
using System.Collections.Generic;

namespace WorkTime.Models;

public partial class BankInformation
{
    public string Id { get; set; } = null!;

    public string BankAccount { get; set; } = null!;

    public string BankName { get; set; } = null!;

    public string BankLocation { get; set; } = null!;

    public string CorInv { get; set; } = null!;

    public string Bik { get; set; } = null!;

    public virtual ICollection<AspNetUserInformation> AspNetUserInformations { get; } = new List<AspNetUserInformation>();

    public virtual ICollection<Company> Companies { get; } = new List<Company>();

    public BankInformation()
    {
        Id = Guid.NewGuid().ToString();
    }
}
