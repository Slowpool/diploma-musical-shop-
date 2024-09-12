using DataLayer.NotMapped;
using DataLayer.SupportClasses;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.MusicalInstruments;
public class Synthesizer : Goods
{
    public int SynthesizerId { get; set; }

#warning > 0
    [Required]
    public int KeysNumber { get; set; }
}
