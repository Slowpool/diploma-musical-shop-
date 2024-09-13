using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Miscellaneous;
public class Sale
{
    public int SaleId { get; set; }

    [Required]
    public DateTime Date { get; set; }

#warning it should be calculated via view
    [Required]
    public int Total { get; set; }

#warning each goods must have nullable column int SaleId

}
