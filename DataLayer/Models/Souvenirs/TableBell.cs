using DataLayer.NotMapped;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Souvenirs;
[Table("table_bells")]
public class TableBell : Goods
{
    public int TableBellId { get; set; }
}
