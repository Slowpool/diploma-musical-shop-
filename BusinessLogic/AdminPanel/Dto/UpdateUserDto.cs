using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.AdminPanel.Dto;
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
