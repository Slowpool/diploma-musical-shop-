using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.LinkingTables;
#warning how to make it automatically
[Table("accessory_sale")]
public class AccessorySale
{
    [Required]
    public Guid AccessoryId { get; set; }
    public virtual Accessory Accessory { get; set; }
    [Required]
    public Guid SaleId { get; set; }
    public virtual Sale Sale { get; set; }
}
