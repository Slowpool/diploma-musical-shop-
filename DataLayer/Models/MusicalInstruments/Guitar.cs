using DataLayer.NotMapped;
using DataLayer.SupportClasses;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.MusicalInstruments;

public class Guitar : Goods
{
    #warning probably GUID
    public int GuitarId { get; set; }

#warning > 1
    [Required]
    public int StringsNumber { get; set; }
}
