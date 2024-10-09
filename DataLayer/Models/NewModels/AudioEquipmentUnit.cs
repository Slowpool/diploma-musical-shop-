using DataLayer.NotMapped;
using DataLayer.SupportClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models;

public class AudioEquipmentUnit : Goods
{
    public int AudioEquipmentUnitId { get; set; }
    public SpecificType Type { get; set; }
}
