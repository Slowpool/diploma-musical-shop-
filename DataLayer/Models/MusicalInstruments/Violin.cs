using DataLayer.NotMapped;
using DataLayer.SupportClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.MusicalInstruments;
internal class Violin : MusicalInstrument
{
    public int ViolinId { get; set; }

    [Required]
    public TypeOfViolinStrings TypeOfViolinStrings { get; set; }
}
