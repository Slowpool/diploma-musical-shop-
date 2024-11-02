using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.LinkingTables;
public class MusicalInstrumentSale
{
    [ForeignKey(nameof(MusicalInstrumentSale.MusicalInstrument))]
    [Required]
    public Guid MusicalInstrumentId { get; set; }
    public MusicalInstrument MusicalInstrument { get; set; }
    [ForeignKey(nameof(Sale))]
    [Required]
    public Guid SaleId { get; set; }
    public Sale Sale { get; set; }
}
