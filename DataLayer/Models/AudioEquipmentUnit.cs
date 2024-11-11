using DataLayer.Models.SpecificTypes;
using DataLayer.NotMapped;
using DataLayer.SupportClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models;

[Table("audio_equipment_units")]
public class AudioEquipmentUnit : Goods
{
    //[Column("audio_equipment_unit_id")]
    //public Guid AudioEquipmentUnitId { get; set; }
    public AudioEquipmentUnitSpecificType SpecificType { get; set; }
}
