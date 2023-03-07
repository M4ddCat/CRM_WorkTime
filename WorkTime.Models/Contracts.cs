namespace WorkTime.Models;

public partial class Contracts
{
    public string Id { get; set; } = null!;

    public string ContractNumber { get; set; } = null!;

    public string? PerformerPersonId { get; set; }

    public string? PerformerCompanyId { get; set; }

    public string? CustomerPersonId { get; set; }

    public string? CustomerCompanyId { get; set; }

    public string ProjectId { get; set; } = null!;

    public DateOnly ContractDate { get; set; }

    public virtual ICollection<BankInformation> BankInformation { get; } = new List<BankInformation>();

    public Contracts()
    {
        Id = Guid.NewGuid().ToString();
    }
}