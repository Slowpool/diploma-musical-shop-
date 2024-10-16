using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.SupportClasses;

[Table("specific_type")]
public class SpecificType
{
    [Column("specific_type_id")]
    public int SpecificTypeId { get; set; }

    [Required]
    public string Name { get; set; }
}
