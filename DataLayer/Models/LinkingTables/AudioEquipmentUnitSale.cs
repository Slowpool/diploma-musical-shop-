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
    [ForeignKey(nameof(AudioEquipmentUnitSale.AudioEquipmentUnit))]
    [Required]
    public Guid AudioEquipmentUnitId { get; set; }
    public AudioEquipmentUnit AudioEquipmentUnit { get; set; }
    [ForeignKey(nameof(Sale))]
    [Required]
    public Guid SaleId { get; set; }
    public Sale Sale { get; set; }
}