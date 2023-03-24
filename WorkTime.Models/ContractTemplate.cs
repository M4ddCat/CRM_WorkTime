namespace WorkTime.Models;

public partial class ContractTemplate
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; }

    public byte[]? TemplateFile { get; set; }

    public virtual Project? Project { get; set; }

    public ContractTemplate()
    {
        Id = Guid.NewGuid().ToString();
    }
}