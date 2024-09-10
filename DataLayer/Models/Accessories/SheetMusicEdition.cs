using DataLayer.NotMapped;
using DataLayer.SupportClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Accessories;
internal class SheetMusicEdition : Goods
{
    public int SheetMusicEditionId { get; set; }
    public SheetMusicEditionLevel SheetMusicEditionLevel { get; set; }
}
