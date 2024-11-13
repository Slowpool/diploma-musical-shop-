using DataLayer.NotMapped;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models;

public class GoodsDelivery
{
    public Guid GoodsDeliveryId { get;set; }
    public DateTimeOffset? ExpectedDeliveryDate { get; set; }
    [NotMapped]
    public DateTime? LocalExpectedDeliveryDate
    {
        get => ExpectedDeliveryDate?.LocalDateTime;
        set
        {
            ExpectedDeliveryDate = value.LocalToUniversal();
        }
    }
    public DateTimeOffset? ActualDeliveryDate { get; set; }
    [NotMapped]
    public DateTime? LocalActualDeliveryDate
    {
        get => ActualDeliveryDate?.LocalDateTime;
        set
        {
            ActualDeliveryDate = value.LocalToUniversal();
        }
    }
    [InverseProperty(nameof(Goods.Delivery))]
    public virtual ICollection<MusicalInstrument> MusicalInstruments { get; set; } = [];
    [InverseProperty(nameof(Goods.Delivery))]
    public virtual ICollection<Accessory> Accessories { get; set; } = [];
    [InverseProperty(nameof(Goods.Delivery))]
    public virtual ICollection<AudioEquipmentUnit> AudioEquipmentUnits { get; set; } = [];
    [InverseProperty(nameof(Goods.Delivery))]
    public virtual ICollection<SheetMusicEdition> SheetMusicEditions { get; set; } = [];
}
