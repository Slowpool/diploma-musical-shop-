using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace DataLayer.Models.LinkingTables;
#warning how to make it automatically
//[Table(typeof(AccessorySale).NameToLowerMysql())]
public class AccessorySale
{
    [ForeignKey(nameof(AccessorySale.Accessory))]
    [Required]
    public Guid AccessoryId { get; set; }
    public Accessory Accessory { get; set; }
    [ForeignKey(nameof(Sale))]
    [Required]
    public Guid SaleId { get; set; }
    public Sale Sale { get; set; }
}
