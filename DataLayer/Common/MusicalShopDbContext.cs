using DataLayer.Models;
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
#warning if i don't have a personal life, i could rename all the stuff like here
        //modelBuilder.Entity<IdentityUser>().ToTable("identity_users").Property(u => u.UserId).HasColumnName("user_id");
        modelBuilder.Entity<SaleView>(e =>
        {
            e.ToView("sales_view");
        });

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
