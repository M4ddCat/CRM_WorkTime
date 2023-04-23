namespace WorkTime.Models;

public partial class ContractTemplate
{
    public string Id { get; set; } = null!;

    public string? ProjectId { get; set; }

    public string EmpTypeId { get; set; } = null!;

    public string Template { get; set; } = null!;

    public virtual Project? Project { get; set; }

    public virtual TypeOfEmployment? TypeOfEmployment { get; set; }

    public ContractTemplate()
    {
        Id = Guid.NewGuid().ToString();
    }
}