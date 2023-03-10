namespace WorkTime.Models;

public partial class LegalUserType
{
    public int Id { get; set; }

    public string LegalType { get; set; } = null!;

    public virtual ICollection<AspNetUserInformation> AspNetUserInformations { get; } = new List<AspNetUserInformation>();

}
