using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.SupportClasses;

namespace DataLayer.Models;
[Table("sales")]
public class Sale
{
    [Column("sale_id")]
    public Guid SaleId { get; set; }

    [Required]
    public DateTimeOffset Date { get; set; }

#warning is it working correctly?
    public DateTime LocalDate => Date.LocalDateTime;

#warning how to add refers to several tables like ICollection<Goods> Answer: view
    [Required]
    public SaleStatus Status { get; set; }
}
