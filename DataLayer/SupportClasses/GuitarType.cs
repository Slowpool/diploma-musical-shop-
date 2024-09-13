using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.SupportClasses;
[Table("guitar_types")]
public class GuitarType
{
    public int GuitarTypeId { get; set; }
    [Required]
    public string Type { get; set; }
    ///// <summary>
    ///// Classic guitar has a nylon strings and 12 frets. (roughly)
    ///// </summary>
    //Classic,
    ///// <summary>
    ///// Acoustic guitar has steel strings at least.
    ///// </summary>
    //Acoustic,
    ///// <summary>
    ///// Electric guitar generates sound using electricity.
    ///// </summary>
    //Electric
}
