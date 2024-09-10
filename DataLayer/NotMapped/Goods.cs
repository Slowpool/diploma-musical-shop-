using DataLayer.SupportClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.NotMapped;

[NotMapped]
public class Goods : ISoftDeletable
{
    public bool SoftDeleted { get; set; }
#warning > 0
    public int Price { get; set; }
}
