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
    [Required]
    public Guid MusicalInstrumentId { get; set; }
    public virtual MusicalInstrument MusicalInstrument { get; set; }
    [Required]
    public Guid SaleId { get; set; }
    public virtual Sale Sale { get; set; }
}
