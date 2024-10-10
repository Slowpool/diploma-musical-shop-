using DataLayer.Models;
using DataLayer.Models.Accessories;
using DataLayer.Models.AudioEquipment;
using DataLayer.Models.MusicalInstruments;
using DataLayer.Models.Souvenirs;
using DataLayer.NotMapped;
using DataLayer.SupportClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Common;
public class MusicalShopContext : DbContext
{
    public virtual DbSet<MusicalInstrument> MusicalInstruments { get; set; }
    public virtual DbSet<Accessory> Accessories { get; set; }
    public virtual DbSet<AudioEquipmentUnit> AudioEquipmentUnits { get; set; }
    public virtual DbSet<SheetMusicEdition> SheetMusicEditions { get; set; }
    public virtual DbSet<SpecificType> SpecificTypes { get; set; }
    public virtual DbSet<Sale> Sales { get; set; }
    


    public MusicalShopContext() : base()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
        //InsertTypeNames();
    }

    public MusicalShopContext(DbContextOptions options) : base(options)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#warning and how to change the user here corresponding to the current user? or i don't need it? because there (on server) will be authentication
        optionsBuilder.UseMySql("database=musical_shop;server=localhost;port=3306;user=root;password=password;", ServerVersion.Parse("8.0.39"));
        //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MusicalShop;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Ignore<Goods>();
        //modelBuilder.Ignore<MusicalInstrument>();

        var entities = modelBuilder.Model.GetEntityTypes();
        foreach (var entity in entities)
        {
            ConfigurePropertiesViaFluentApi(entity);
        }
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
                        var methodToCall = typeof(MusicalShopContext)
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
        where TEntity: class, ISoftDeletable
    {
        Expression<Func<TEntity, bool>> filter = e => !e.SoftDeleted;
        return filter;
    }
}
