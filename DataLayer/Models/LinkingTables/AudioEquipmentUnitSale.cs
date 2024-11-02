using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.LinkingTables;
public class AudioEquipmentUnitSale
{
    [Required]
    public Guid AudioEquipmentUnitId { get; set; }
    public virtual AudioEquipmentUnit AudioEquipmentUnit { get; set; }
    [Required]
    public Guid SaleId { get; set; }
    public virtual Sale Sale { get; set; }
}