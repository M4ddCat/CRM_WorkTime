namespace WorkTime.Models;

public partial class UserType
{
    public string Id { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string PersonType { get; set; } = null!;

    public virtual AspNetUserInformation UserInfo { get; set; } = null!;

    public UserType()
    {
        Id = Guid.NewGuid().ToString();
    }
}