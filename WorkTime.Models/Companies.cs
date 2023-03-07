namespace WorkTime.Models;

public partial class Companies
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string BankInfoId { get; set; } = null!;

    public string DirectorFullName { get; set; } = null!;

    public string CompanyPlace { get; set; } = null!;

    public virtual ICollection<BankInformation> BankInformation { get; } = new List<BankInformation>();

    public Companies()
    {
        Id = Guid.NewGuid().ToString();
    }
}