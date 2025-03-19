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
    [Key]
    public Guid ReservationExtraInfoId { get; set; }
    [Required]
    public string SecretWord { get; set; }
    // TODO make it required everywhere
    public bool SoftDeleted { get; set; }
    // relationships
    [Required]
    [InverseProperty(nameof(Sale.ReservationExtraInfo))]
    public Sale Sale { get; set; }
}
