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
    //[Column("goods_id")]
    public Guid GoodsId { get; set; }
    //[Column("soft_deleted")]
    public bool SoftDeleted { get; set; }
#warning workaround
    [Range(0, 4_294_967_295)]
    public int Price { get; set; }
    public GoodsStatus Status { get; set; }
    [MaxLength(500)]
    public string Description { get; set; }
    [MaxLength(200)]
    public string Name { get; set; }
    // relationships
    //[Column("sale_id")]
    //[ForeignKey(nameof(Goods.Sale))]
    //public Guid? SaleId { get; set; }
    public virtual ICollection<Sale> Sales { get; set; } = [];
    //[Column("type_id")]
    public Guid SpecificTypeId { get; set; }
    //public abstract SpecificType SpecificType { get; set; }
    //[Column("receipt_date")]
    public DateTimeOffset? ReceiptDate { get; set; }
    public Guid? DeliveryId { get; set; }
    [ForeignKey(nameof(Goods.DeliveryId))]
    public virtual GoodsDelivery? Delivery { get; set; }

}
