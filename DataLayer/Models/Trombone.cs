using DataLayer.NotMapped;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models;
public class Trombone : Goods
{
    public int TromboneId { get; set; }
    public bool SlidePresence { get; set; }
}
