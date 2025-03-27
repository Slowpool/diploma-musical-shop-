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
public class SheetMusicEdition : Goods
{
    [MaxLength(Consts.SheetMusicEditionAuthorMaxLength)]
    public string? Author { get; set; }
    public int ReleaseYear { get; set; }
    public SheetMusicEditionSpecificType SpecificType { get; set; }
}
