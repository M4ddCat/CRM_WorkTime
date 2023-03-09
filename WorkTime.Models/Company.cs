using System;
using System.Collections.Generic;

namespace WorkTime.Models;

public partial class Company
{
    public string Id { get; set; } = null!;

    public string BankInfoId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string DirectorFullName { get; set; } = null!;

    public string CompanyPlace { get; set; } = null!;

    public virtual ICollection<AspNetUserInformation> AspNetUserInformations { get; } = new List<AspNetUserInformation>();

    public virtual BankInformation BankInfo { get; set; } = null!;

    public virtual ICollection<Contract> ContractCustomerCompanies { get; } = new List<Contract>();

    public virtual ICollection<Contract> ContractPerformerCompanies { get; } = new List<Contract>();

    public Company()
    {
        Id = Guid.NewGuid().ToString();
    }
}
