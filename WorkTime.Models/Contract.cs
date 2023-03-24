namespace WorkTime.Models;

public partial class Contract
{
    public string Id { get; set; } = null!;

    public string ContractNumber { get; set; } = null!;

    public string? PerformerPersonId { get; set; }

    public string? PerformerCompanyId { get; set; }

    public string? CustomerPersonId { get; set; }

    public string? CustomerCompanyId { get; set; }

    public string UserProjectId { get; set; } = null!;

    public DateTime? ContractDate { get; set; }

    public byte[]? ContractFile { get; set; }

    public virtual Company? CustomerCompany { get; set; }

    public virtual AspNetUser? CustomerPerson { get; set; }

    public virtual Company? PerformerCompany { get; set; }

    public virtual AspNetUser? PerformerPerson { get; set; }

    public virtual UserProject UserProject { get; set; } = null!;

    public Contract()
    {
        Id = Guid.NewGuid().ToString();
    }
}
