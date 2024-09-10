using DataLayer.NotMapped;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Users;
internal class User
{
    public int UserId { get; set; }
    public string Login { get; set; }
#warning I STILL DON'T KNOW WHERE TO STORE DYNAMIC SAULT
#warning 64 or 128 characters
    public string HashedPassword { get; set; }
    public UserPrivileges UserPrivileges { get; set; }
}
