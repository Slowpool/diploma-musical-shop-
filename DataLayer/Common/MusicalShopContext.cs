using DataLayer.Models;
using DataLayer.Models.Accessories;
using DataLayer.Models.MusicalInstruments;
using DataLayer.NotMapped;
using DataLayer.SupportClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Common;
internal class MusicalShopContext : DbContext
{
    // musical instruments
    public virtual DbSet<Guitar> Guitars { get; set; }
    public virtual DbSet<Trombone> Trombones { get; set; }
    public virtual DbSet<Synthesizer> Synthesizers { get; set; }
    public virtual DbSet<Violin> Violins { get; set; }
    // accessories
    public virtual DbSet<MusicStand> MusicStands { get; set; }
    public virtual DbSet<SheetMusicEdition> SheetMusicEditions { get; set; }
    public virtual DbSet<Chair> Chairs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public MusicalShopContext() : base()
    {

    }

    public MusicalShopContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var entities = modelBuilder.Model.GetEntityTypes();
        foreach (var entity in entities)
        {
            var softDeletedProperty = entity.GetProperty(nameof(ISoftDeletable.SoftDeleted));
            if (softDeletedProperty != null)
            {
#warning i'm not sure it's working
                Expression<Func<ISoftDeletable, bool>> filter = e => !e.SoftDeleted;
                entity.SetQueryFilter(filter);
            }

            var priceProperty = entity.GetProperty(nameof(Goods.Price));
            if (priceProperty != null)
            {
                entity.SetAnnotation();
            }
        }
    }
}
