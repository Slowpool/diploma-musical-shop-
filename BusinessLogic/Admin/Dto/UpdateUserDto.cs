namespace BusinessLogicLayer.Admin.Dto;
#warning i think this is inappropriate place for dto
public class UpdateUserDto
{
    public Guid Id { get; set; }
    public string? UserName { get; set; }
    public bool EmailConfirmed { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public DateTime? LockoutEnd { get; set; }
    public bool LockoutEnabled { get; set; }
}
