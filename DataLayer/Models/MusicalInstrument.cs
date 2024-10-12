using DataLayer.NotMapped;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models;
[Table("musical_instruments")]
public class MusicalInstrument : Goods
{
    [Column("musical_instrument_id")]
    public int MusicalInstrumentId { get; set; }
#warning [Range(0, )] // postponed to business logic
    [Column("release_year")]
    public int ReleaseYear { get; set; }
}
