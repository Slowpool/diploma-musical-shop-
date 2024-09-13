using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.SupportClasses;

[Table("trombone_types")]
public class TromboneType
{
    public int TromboneTypeId { get; set; }
    [Required]
    public string Type { get; set; }
    //Tenor,
    //Alto,
    //Bass
}
