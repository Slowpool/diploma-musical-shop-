using DataLayer.NotMapped;
using DataLayer.SupportClasses;

namespace DataLayer.Models;

public class Guitar : Goods
{
    // probably GUID
    public int GuitarId { get; set; }
    // > 1
    public int StringsNumber { get; set; }
    // enable casting to string in db
    public GuitarType GuitarType { get; set; }

}
