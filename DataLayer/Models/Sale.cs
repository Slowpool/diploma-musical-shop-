using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models;
public class Sale
{
    public int SaleId { get; set; }

    [Required]
    public DateTimeOffset Date { get; set; }

#warning is it working correctly?
    public DateTime LocalTime => Date.LocalDateTime;

#warning it should be calculated via view
    [Required]
    public int Total { get; set; }

#warning how to add refers to several tables like ICollection<Goods>
    [Required]
    public SaleStatus Status { get; set; }
}
