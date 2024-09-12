using DataLayer.NotMapped;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Accessories;
public class MusicStand : Goods
{
    public int MusicStandId { get; set; }
#warning > 0, <= MaxHeight
    public int MinHeight { get; set; }
#warning > 0, >= MinHeight
    public int MaxHeight { get; set; }
}
