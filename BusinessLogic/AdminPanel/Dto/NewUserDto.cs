using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.AdminPanel.Dto;

public record class NewUserDto(string? UserName, string? Email, bool EmailConfirmed, string? PhoneNumber, bool PhoneNumberConfirmed, string Password);