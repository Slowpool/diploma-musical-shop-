
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.SupportClasses;

[Table("type_of_violin_strings")]
public class TypeOfViolinStrings
{
    public int TypeOfViolinStringsId { get; set; }
    [Required]
    public string Type { get; set; }
    //Steel,
    //Gut
}
