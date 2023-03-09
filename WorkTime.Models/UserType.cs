namespace WorkTime.Models;

public partial class UserType
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public string PersonType { get; set; } = null!;

    public virtual ICollection<AspNetUserInformation> AspNetUserInformations { get; } = new List<AspNetUserInformation>();

}
