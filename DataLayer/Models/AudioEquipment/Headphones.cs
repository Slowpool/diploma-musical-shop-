using DataLayer.NotMapped;
using DataLayer.SupportClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.AudioEquipment;
internal class Headphones : Goods
{
    public int HeadphonesId { get; set; }
    public HeadphonesType HeadphonesType { get; set; }
}
