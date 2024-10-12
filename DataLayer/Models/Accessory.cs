using DataLayer.NotMapped;
using DataLayer.SupportClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models;

[Table("accessories")]
public class Accessory : Goods
{
    [Column("accessory_id")]
    public int AccessoryId { get; set; }
    [Column("type_id")]
    public int TypeId { get; set; }
    public SpecificType Type { get; set; }

}
