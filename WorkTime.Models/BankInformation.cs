namespace WorkTime.Models;

public partial class BankInformation
{
    public string Id { get; set; } = null!;

    public string BankAccount { get; set; } = null!;

    public string BankName { get; set; } = null!;

    public string BankLocation { get; set; } = null!;

    public string BIK { get; set; } = null!;

    public string CorInv { get; set; } = null!;

    public virtual AspNetUserInformation? UserInfo { get; set; }

    public virtual Companies? Companies { get; set; }

    public BankInformation()
    {
        Id = Guid.NewGuid().ToString();
    }
}