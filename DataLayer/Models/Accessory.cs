using DataLayer.Models.SpecificTypes;
using DataLayer.NotMapped;
using DataLayer.SupportClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models;

[Table("accessories")]
public class Accessory : Goods
{
    public string Color { get; set; }
    //[Required]
    //[MaxLength(255)]
    public string Size { get; set; }
    public AccessorySpecificType SpecificType { get; set; }
}
