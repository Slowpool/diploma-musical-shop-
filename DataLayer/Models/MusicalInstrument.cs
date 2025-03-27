using DataLayer.Models.SpecificTypes;
using DataLayer.NotMapped;
using DataLayer.SupportClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models;
public class MusicalInstrument : Goods
{
    public int ReleaseYear { get; set; }
    [MaxLength(Consts.MusicalInstrumentManufacturerMaxLength)]
    public string Manufacturer { get; set; }
    public ManufacturerType ManufacturerType { get; set; }
    public MusicalInstrumentSpecificType SpecificType { get; set; }
}
