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
public class Goods : ISoftDeletable
{
    [Key]
    [Column("goods_id")]
    public Guid GoodsId { get; set; }
    [Column("soft_deleted")]
    public bool SoftDeleted { get; set; }
#warning workaround
    [Range(0, 10_000_000)]
    public int Price { get; set; }
    public GoodsStatus Status { get; set; }
    [MaxLength(500)]
    public string Description { get; set; }
    // relationships
    [Column("sale_id")]
    public int? SaleId { get; set; }
    public Sale? Sale { get; set; }
    [Column("type_id")]
    public int TypeId { get; set; }
    public SpecificType Type { get; set; }
    [Column("receipt_date")]
    public DateTimeOffset? ReceiptDate { get; set; }

}
