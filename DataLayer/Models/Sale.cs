using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.SupportClasses;

namespace DataLayer.Models;
[Table("sales")]
public class Sale
{
    //[Column("sale_id")]
    public Guid SaleId { get; set; }
    //[Column("sale_date")]
    public DateTimeOffset? SaleDate { get; set; }
    //[Column("reservation_date")]
    public DateTimeOffset? ReservationDate { get; set; }
    //[Column("returning_date")]
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
    //[Column("paid_by")]
    public SalePaidBy PaidBy { get; set; }
    [Required]
    //[Column("is_paid")]
    public bool IsPaid { get; set; }
    // relationships
    public virtual ICollection<MusicalInstrument> MusicalInstruments { get; set; } = [];
    public virtual ICollection<Accessory> Accessories { get; set; } = [];
    public virtual ICollection<AudioEquipmentUnit> AudioEquipmentUnits { get; set; } = [];
    public virtual ICollection<SheetMusicEdition> SheetMusicEditions { get; set; } = [];
}
