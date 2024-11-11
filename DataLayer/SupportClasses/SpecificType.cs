using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.SupportClasses;

[NotMapped]
public abstract class SpecificType
{
    [Key]
    public Guid SpecificTypeId { get; set; }

    [Required]
    public string Name { get; set; }
}
