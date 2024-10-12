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
[Table("sheet_music_editions")]
public class SheetMusicEdition : Goods
{
    [Column("sheet_music_edition_id")]
    public int SheetMusicEditionId { get; set; }
}
