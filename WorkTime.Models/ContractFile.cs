namespace WorkTime.Models;

public partial class ContractFile
{
    public string Id { get; set; } = null!;

    public string ContractId { get; set; } = null!;

    public byte[]? File { get; set; }

    public virtual Contract? Contract { get; set; }

    public ContractFile()
    {
        Id = Guid.NewGuid().ToString();
    }
}
