using DataLayer.SupportClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models;

[Table("sale_view")]
public class SaleView
{
    [Column("sale_id")]
    public int SaleId { get; set; }

    [Required]
    public DateTimeOffset Date { get; set; }

#warning is it working correctly?
    public DateTime LocalDate => Date.LocalDateTime;

    //[Required]
    //public int Total { get; set; }

#warning how to add refers to several tables like ICollection<Goods> Answer: view
    [Required]
    public SaleStatus Status { get; set; }
}
