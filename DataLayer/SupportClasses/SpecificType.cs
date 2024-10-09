using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.SupportClasses;

[Table("specific_type")]
public class SpecificType
{
    public int SpecificTypeId { get; set; }

    [Required]
    public string Type { get; set; }
}
