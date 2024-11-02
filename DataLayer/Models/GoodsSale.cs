using DataLayer.NotMapped;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models;
public class GoodsSale
{
    [ForeignKey(nameof(GoodsUnit))]
    [Required]
    public Guid GoodsId { get; set; }
    public Goods GoodsUnit { get; set; }
    [ForeignKey(nameof(Sale))]
    [Required]
    public Guid SaleId { get; set; }
    public Sale Sale { get; set; }
}
