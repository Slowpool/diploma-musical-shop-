using DataLayer.Models;
using DataLayer.Models.Accessories;
using DataLayer.Models.AudioEquipment;
using DataLayer.Models.MusicalInstruments;
using DataLayer.Models.Souvenirs;
using DataLayer.NotMapped;
using DataLayer.SupportClasses;
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
public class MusicalShopContext : DbContext
{
    // musical instruments
    public virtual DbSet<Guitar> Guitars { get; set; }
    public virtual DbSet<Trombone> Trombones { get; set; }
    public virtual DbSet<Synthesizer> Synthesizers { get; set; }
    public virtual DbSet<Violin> Violins { get; set; }

    // accessories
    public virtual DbSet<MusicStand> MusicStands { get; set; }
    public virtual DbSet<Tuner> Tuners { get; set; }
    public virtual DbSet<Chair> Chairs { get; set; }

    // audio equipment
    public virtual DbSet<Headphones> Headphones { get; set; }
    public virtual DbSet<Microphone> Microphones { get; set; }
    // souvenirs
    public virtual DbSet<TableBell> TableBells { get; set; }
    public virtual DbSet<Keychain> Keychains { get; set; }
    //
    public virtual DbSet<SheetMusicEdition> SheetMusicEditions { get; set; }
    // technical data
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserPrivileges> UserPrivileges { get; set; }


    public MusicalShopContext() : base()
    {
        Database.EnsureCreated();
        //Database.EnsureDeleted();

    }

    public MusicalShopContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
        //Database.EnsureDeleted();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#warning and how to change the user here corresponding to the current user? or i don't need it? because there (on server) will be authentication
        //optionsBuilder.UseMySql("database=musical_shop;server=localhost;port=3306;user=root;password=password;", ServerVersion.Parse("8.0.39"));
        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MusicalShop;Trusted_Connection=True;");
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
