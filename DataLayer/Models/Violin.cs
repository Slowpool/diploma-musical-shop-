using DataLayer.NotMapped;
using DataLayer.SupportClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models;
internal class Violin : Goods
{
    public int ViolinId { get; set; }
    public TypeOfViolinStrings TypeOfViolinStrings { get;set;}
}
