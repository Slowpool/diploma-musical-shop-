using DataLayer.NotMapped;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.MusicalInstruments;
public class Trombone : Goods
{
    public int TromboneId { get; set; }

    [Required]
    public bool SlidePresence { get; set; }
}
