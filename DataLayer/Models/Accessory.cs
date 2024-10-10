using DataLayer.NotMapped;
using DataLayer.SupportClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models;

public class Accessory : Goods
{
    public int AccessoryId { get; set; }
    public SpecificType Type { get; set; }

}
