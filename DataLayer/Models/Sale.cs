using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.NotMapped;
using DataLayer.SupportClasses;

namespace DataLayer.Models;
[Table("sales")]
public class Sale : ISoftDeletable
{
    public Guid SaleId { get; set; }
    public DateTimeOffset? SaleDate { get; set; }
    public DateTimeOffset? ReservationDate { get; set; }
    public DateTimeOffset? ReturningDate { get; set; }
#warning is it working correctly?
    [NotMapped]
    public DateTime? LocalSaleDate
    {
        get => SaleDate?.LocalDateTime;
        set
        {
            SaleDate = value.LocalToUniversal();
        }
    }
    [NotMapped]
    public DateTime? LocalReservationDate
    {
        get => ReservationDate?.LocalDateTime;
        set
        {
            ReservationDate = value.LocalToUniversal();
        }
    }
    [NotMapped]
    public DateTime? LocalReturningDate
    {
        get => ReturningDate?.LocalDateTime;
        set
        {
            ReturningDate = value.LocalToUniversal();
        }
    }
#warning how to add references to several tables like ICollection<Goods> Answer: view. Upd: bad answer.
    [Required]
    public SaleStatus Status { get; set; }
    [Required]
    public bool IsPaid { get; set; }
    public SalePaidBy? PaidBy { get; set; }
    public bool SoftDeleted { get; set; }
    // relationships
    public virtual ICollection<MusicalInstrument> MusicalInstruments { get; set; } = [];
    public virtual ICollection<Accessory> Accessories { get; set; } = [];
    public virtual ICollection<AudioEquipmentUnit> AudioEquipmentUnits { get; set; } = [];
    public virtual ICollection<SheetMusicEdition> SheetMusicEditions { get; set; } = [];
    // TODO configure via fluent api to add on_delete cascade
    public Guid? ReservationExtraInfoId { get; set; }
    [ForeignKey(nameof(Sale.ReservationExtraInfoId))]
    public virtual ReservationExtraInfo? ReservationExtraInfo { get; set; } = null;
}
