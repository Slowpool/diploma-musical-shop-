using DataLayer.SupportClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models;

[Keyless]
public class SaleView
{
    [Column("sale_id")]
    public Guid SaleId { get; set; }

    [Column("sale_date")]
    public DateTimeOffset? SaleDate { get; set; }
    [Column("reservation_date")]
    public DateTimeOffset? ReservationDate { get; set; }
    [Column("returning_date")]
    public DateTimeOffset? ReturningDate { get; set; }
    [NotMapped]
    public DateTime? LocalSaleDate => SaleDate?.LocalDateTime;
    [NotMapped]
    public DateTime? LocalReservationDate => ReservationDate?.LocalDateTime;
    [NotMapped]
    public DateTime? LocalReturningDate => ReturningDate?.LocalDateTime;
    public int Total { get; set; }
#warning how to add refers to several tables like ICollection<Goods> Answer: view
    [Required]
    public SaleStatus Status { get; set; }
    [Column("paid_by")]
    public SalePaidBy? PaidBy { get; set; }
    [Column("goods_units_count")]
    public int? GoodsUnitsCount { get; set; }
    [Required]
    [Column("is_paid")]
    public bool IsPaid { get; set; }
    [NotMapped]
    public IReadOnlyCollection<MusicalInstrument> MusicalInstruments { get; set; } = [];
    [NotMapped]
    public IReadOnlyCollection<Accessory> Accessories { get; set; } = [];
    [NotMapped]
    public IReadOnlyCollection<AudioEquipmentUnit> AudioEquipmentUnits { get; set; } = [];
    [NotMapped]
    public IReadOnlyCollection<SheetMusicEdition> SheetMusicEditions { get; set; } = [];
}
