﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Miscellaneous;
[Table("user_privileges")]
public class UserPrivileges
{
    public int UserPrivilegesId { get; set; }
    public string Name { get; set; }

#warning should i extend it with bool values like "AllowedToArrangeASale"?
}