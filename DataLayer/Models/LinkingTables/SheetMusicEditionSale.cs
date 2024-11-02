using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.LinkingTables;
public class SheetMusicEditionSale
{
    [ForeignKey(nameof(SheetMusicEditionSale.SheetMusicEdition))]
    [Required]
    public Guid SheetMusicEditionId { get; set; }
    public SheetMusicEdition SheetMusicEdition { get; set; }
    [ForeignKey(nameof(Sale))]
    [Required]
    public Guid SaleId { get; set; }
    public Sale Sale { get; set; }
}
