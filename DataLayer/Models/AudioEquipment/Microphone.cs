using DataLayer.NotMapped;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.AudioEquipment;
internal class Microphone : Goods
{
    public int MicrophoneId { get; set; }
    [MaxLength(255)]
    public string? Description { get; set; }
}
