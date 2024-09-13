using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.SupportClasses;

[Table("headphones_types")]
public class HeadphonesType
{
    public int HeadphonesTypeId { get; set; }

    [Required]
    public string Type { get; set; }
    ///// <summary>
    ///// Big headphones which cover whole the ears. (own idea)
    ///// </summary>
    //Covering, // 
    ///// <summary>
    ///// Little headphones with a size of dewdrop. (still own idea)
    ///// </summary>
    //Dewdrops // 
}
