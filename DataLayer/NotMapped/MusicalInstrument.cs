using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.NotMapped;
[NotMapped]
internal class MusicalInstrument : Goods
{
    //[Range(0, )] // postponed to business logic
    public int ReleaseYear { get; set; }
}
