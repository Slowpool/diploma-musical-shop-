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
    [Required]
    public Guid SheetMusicEditionId { get; set; }
    public virtual SheetMusicEdition SheetMusicEdition { get; set; }
    [Required]
    public Guid SaleId { get; set; }
    public virtual Sale Sale { get; set; }
}
