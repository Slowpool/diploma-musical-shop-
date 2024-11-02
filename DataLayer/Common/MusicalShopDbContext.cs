using DataLayer.Models;
using DataLayer.Models.LinkingTables;
using DataLayer.NotMapped;
using DataLayer.SupportClasses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Common;
public partial class MusicalShopDbContext : IdentityDbContext<AppUser>
{
    public virtual DbSet<MusicalInstrument> MusicalInstruments { get; set; }
    public virtual DbSet<Accessory> Accessories { get; set; }
    public virtual DbSet<AudioEquipmentUnit> AudioEquipmentUnits { get; set; }
    public virtual DbSet<SheetMusicEdition> SheetMusicEditions { get; set; }
    public virtual DbSet<SpecificType> SpecificTypes { get; set; }
    public virtual DbSet<Sale> Sales { get; set; }
    public virtual DbSet<SaleView> SalesView { get; set; }

    public MusicalShopDbContext(DbContextOptions<MusicalShopDbContext> options)
        : base(options)
    {
        //RebuildDb();
    }

    public MusicalShopDbContext() : base()
    {
        //RebuildDb();
    }

    private void RebuildDb()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#warning how to change the user here corresponding to the current user? or i don't need it? because there (on server) will be authentication
        //optionsBuilder.UseSnakeCaseNamingConvention();
        if (optionsBuilder.IsConfigured)
            return;
        optionsBuilder.UseMySql("database=musical_shop;server=localhost;port=3306;user=root;password=password;", ServerVersion.Parse("8.0.39"));
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<GoodsStatus>()
                            .HaveConversion<string>();

        configurationBuilder.Properties<SaleStatus>()
                            .HaveConversion<string>();

        configurationBuilder.Properties<SalePaidBy>()
                            .HaveConversion<string>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
#warning if i don't have a personal life, i could rename all stuff like here
        //modelBuilder.Entity<IdentityUser>().ToTable("identity_users").Property(u => u.UserId).HasColumnName("user_id");
        modelBuilder.Entity<SaleView>(entity =>
        {
            entity.ToView("sales_view");
        });

        #region many-to-many sales and goods
#warning well, i'm able to implement it, but i wanna something like single linking table like goods_sale
        modelBuilder.Entity<MusicalInstrument>(entity =>
        {
            entity.HasMany(mi => mi.Sales)
                  .WithMany(sale => sale.MusicalInstruments)
                  .UsingEntity<MusicalInstrumentSale>(linkingTable =>
                  {
                      linkingTable.HasKey(lt => new { lt.SaleId, lt.MusicalInstrumentId });
                      linkingTable.HasOne(e => e.MusicalInstrument)
                                  .WithMany()
                                  .HasForeignKey(e => e.MusicalInstrumentId)
                                  .HasConstraintName("FK_sale_musical_instrument_id");
                      linkingTable.HasOne(e => e.Sale)
                                  .WithMany()
                                  .HasForeignKey(e => e.SaleId)
                                  .HasConstraintName("FK_sale_musical_instrument_sale_id");
                  });
        });

        modelBuilder.Entity<Accessory>(entity =>
        {
            entity.HasMany(a => a.Sales)
                  .WithMany(sale => sale.Accessories)
                  .UsingEntity<AccessorySale>(linkingTable =>
                  {
                      linkingTable.HasKey(lt => new { lt.SaleId, lt.AccessoryId });
                      linkingTable.HasOne(e => e.Accessory)
                                  .WithMany()
                                  .HasForeignKey(e => e.AccessoryId)
                                  .HasConstraintName("FK_sale_accessory_id");
                      linkingTable.HasOne(e => e.Sale)
                                  .WithMany()
                                  .HasForeignKey(e => e.SaleId)
                                  .HasConstraintName("FK_sale_accessory_sale_id");
                  });
        });

        modelBuilder.Entity<AudioEquipmentUnit>(entity =>
        {
            entity.HasMany(aeu => aeu.Sales)
                  .WithMany(sale => sale.AudioEquipmentUnits)
                  .UsingEntity<AudioEquipmentUnitSale>(linkingTable =>
                  {
                      linkingTable.HasKey(lt => new { lt.SaleId, lt.AudioEquipmentUnitId });
                      linkingTable.HasOne(e => e.AudioEquipmentUnit)
                                  .WithMany()
                                  .HasForeignKey(e => e.AudioEquipmentUnitId)
                                  .HasConstraintName("FK_sale_aeu_id");
                      linkingTable.HasOne(e => e.Sale)
                                  .WithMany()
                                  .HasForeignKey(e => e.SaleId)
                                  .HasConstraintName("FK_sale_aeu_sale_id");
                  });
        });

        modelBuilder.Entity<SheetMusicEdition>(entity =>
        {
            entity.HasMany(sme => sme.Sales)
                  .WithMany(sale => sale.SheetMusicEditions)
                  .UsingEntity<SheetMusicEditionSale>(linkingTable =>
                  {
                      linkingTable.HasKey(lt => new { lt.SaleId, lt.SheetMusicEditionId });
                      linkingTable.HasOne(e => e.SheetMusicEdition)
                                  .WithMany()
                                  .HasForeignKey(e => e.SheetMusicEditionId)
                                  .HasConstraintName("FK_sale_sme_id");
                      linkingTable.HasOne(e => e.Sale)
                                  .WithMany()
                                  .HasForeignKey(e => e.SaleId)
                                  .HasConstraintName("FK_sale_sme_sale_id");
                  });
        });
        #endregion

        base.OnModelCreating(modelBuilder);
        //var entities = modelBuilder.Model.GetEntityTypes();
        //foreach (var entity in entities)
        //{
        //    ConfigurePropertiesViaFluentApi(entity);
        //}
    }

    private void ConfigurePropertiesViaFluentApi(IMutableEntityType entity)
    {
        var properties = entity.GetProperties();

        foreach (var property in properties)
        {
            switch (property.Name)
            {
                case nameof(ISoftDeletable.SoftDeleted):
                    {
#warning i'm not sure it's working
                        var methodToCall = typeof(MusicalShopDbContext)
                                               .GetMethod("GetSoftDeletedFilter", BindingFlags.NonPublic | BindingFlags.Static)!
                                               .MakeGenericMethod(entity.ClrType);
                        var filter = methodToCall.Invoke(null, []);
                        entity.SetQueryFilter((LambdaExpression)filter!);
                        break;
                    }
                case nameof(Goods.Price):
                    {
#warning how to do it (now i've implemented it via workaround)
                        //priceProperty.;
                        break;
                    }
                default:
                    break;

            }
        }
    }

    private static Expression<Func<TEntity, bool>> GetSoftDeletedFilter<TEntity>()
        where TEntity : class, ISoftDeletable
    {
        Expression<Func<TEntity, bool>> filter = e => !e.SoftDeleted;
        return filter;
    }
}
