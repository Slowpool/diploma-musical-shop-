using DataLayer.Models;
using DataLayer.SupportClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.NotMapped;

//[NotMapped] // Apparently ef core ignores types which are inhereted by other types and doesn't model them even without this attribute. But I wrote it here just for clarity.
public abstract class Goods : ISoftDeletable
{
    [Key]
    public Guid GoodsId { get; set; }
    [Required]
    [MaxLength(ConstValues.MaxGoodsNameLength)]
    public string Name { get; set; }
    public bool SoftDeleted { get; set; }
    [Range(ConstValues.MinGoodsPriceValue, ConstValues.MaxPriceValue)]
    public int Price { get; set; }
    public GoodsStatus Status { get; set; }
    [MaxLength(ConstValues.MaxGoodsDescriptionLength)]
    public string? Description { get; set; }
    // relationships
    public virtual ICollection<Sale> Sales { get; set; } = [];
    public Guid SpecificTypeId { get; set; }
    //public abstract SpecificType SpecificType { get; set; }
    public DateTimeOffset? ReceiptDate { get; set; }
    public Guid? DeliveryId { get; set; }
    [ForeignKey(nameof(Goods.DeliveryId))]
    public virtual GoodsDelivery? Delivery { get; set; }
}
