using DataLayer.NotMapped;
using DataLayer.SupportClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.CommonExtensions;

namespace DataLayer.Models;

public class ReservationExtraInfo : ISoftDeletable
{
    [Required]
    public string SecretWord { get; set; }
    public bool SoftDeleted { get; set; }
    // relationships
    [Key]
    [Required]
    public Guid SaleId { get; set; }
    [Required]
    [ForeignKey(nameof(ReservationExtraInfo.SaleId))]
    public Sale Sale { get; set; }


}
