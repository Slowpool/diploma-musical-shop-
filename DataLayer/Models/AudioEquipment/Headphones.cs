using DataLayer.NotMapped;
using DataLayer.SupportClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.AudioEquipment;
public class Headphones : Goods
{
    public int HeadphonesId { get; set; }
    [Required]
    public HeadphonesType HeadphonesType { get; set; }
}
