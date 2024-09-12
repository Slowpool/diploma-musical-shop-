using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.NotMapped;
[NotMapped] // Apparently ef core ignores types which are inhereted by other types and doesn't model them even without this attribute. But I wrote it here just for clarity.
public class MusicalInstrument : Goods
{
#warning [Range(0, )] // postponed to business logic
    public int ReleaseYear { get; set; }
}
