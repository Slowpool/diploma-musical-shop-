using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.SupportClasses;
[Table("sheet_music_edition_levels")]
public class SheetMusicEditionLevel
{
    public int SheetMusicEditionLevelId { get; set; }
    public string Level { get; set; }
    //Beginner,
    //Average,
    //Professional,
    //Virtuoso
}
