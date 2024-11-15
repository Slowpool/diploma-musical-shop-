using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Admin.Dto;
#warning i think this is inappropriate place for dto
public record class NewUserDto(string? UserName, string? Email, bool EmailConfirmed, string? PhoneNumber, bool PhoneNumberConfirmed, string Password);