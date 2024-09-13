using DataLayer.NotMapped;
using DataLayer.SupportClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Miscellaneous;
[Table("sheet_music_editions")]
public class SheetMusicEdition : Goods
{
    public int SheetMusicEditionId { get; set; }
    [Required]
    public SheetMusicEditionLevel SheetMusicEditionLevel { get; set; }
}
