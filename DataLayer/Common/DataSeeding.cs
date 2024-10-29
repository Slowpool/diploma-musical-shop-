using Common;
using DataLayer.Models;
using DataLayer.SupportClasses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Common;
public class DataSeeding
{
#warning probably use factory here
    private static Sale[] Sales = [
        new()
        {
            SaleId = Guid.Parse("01937ce5-61c0-4eaf-8580-aeeb653b2191"),
            Status = SaleStatus.Sold,
            Date = new DateTime(2023, 10, 1, 10, 49, 07),
            PaidBy = SalePaidBy.BankCard
        },
        new()
        {
            SaleId = Guid.Parse("01938ce5-61c0-4eaf-8580-aeeb653b2191"),
            Status = SaleStatus.Sold,
            Date = new DateTime(2023, 1, 19, 17, 19, 11),
            PaidBy = SalePaidBy.Cash
        },
        new()
        {
            SaleId = Guid.Parse("01939ce5-61c0-4eaf-8580-aeeb653b2191"),
            Status = SaleStatus.Sold,
            Date = new DateTime(2023, 5, 28, 15, 50, 01),
            PaidBy = SalePaidBy.BankCard
        },
        new()
        {
            SaleId = Guid.Parse("01940ce5-61c0-4eaf-8580-aeeb653b2191"),
            Status = SaleStatus.Returned,
            Date = new DateTime(2023, 3, 1, 13, 20, 35),
            PaidBy = SalePaidBy.Cash
        },
        new()
        {
            SaleId = Guid.Parse("01941ce5-61c0-4eaf-8580-aeeb653b2191"),
            Status = SaleStatus.Reserved,
            Date = new DateTime(2023, 7, 9, 10, 11, 35),
            PaidBy = SalePaidBy.BankCard
        },
        ];

#warning seed another data like types of musical instruments like guitars
    public static async Task SeedAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await EnsureRolesAsync(roleManager);

        var userManager = services.GetRequiredService<UserManager<AppUser>>();
        await EnsureUsersAsync(userManager, services);
        // Unneccesary stuff for app, but it provides visibility for test-review
        var context = services.GetRequiredService<MusicalShopDbContext>();
        await EnsureMusicalInstrumentsAndTheirTypes(context);
        await EnsureAccessoriesAndTheirTypes(context);
        await EnsureAudioEquipmentUnitsAndTheirTypes(context);
        await EnsureSheetMusicEditionsAndTheirTypes(context);
    }

    private async static Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        string[] roles = { CommonNames.AdminRole, CommonNames.SellerRole, CommonNames.ConsultantRole, CommonNames.StockManagerRole };
        for (int i = 0; i < 4; i++)
        {
            var roleExists = await roleManager.RoleExistsAsync(roles[i]);
            if (!roleExists)
            {
                await roleManager.CreateAsync(new IdentityRole { Name = roles[i] });
            }
        }
    }

    private async static Task EnsureUsersAsync(UserManager<AppUser> userManager, IServiceProvider services)
    {
        var configuration = services.GetRequiredService<IConfiguration>();
        var passwordsSection = configuration.GetSection("DefaultPasswords");
#warning it could have been implemented more, more simply.
        string[] emails = [CommonNames.DefaultAdminEmail, CommonNames.DefaultSellerEmail, CommonNames.DefaultConsultantEmail, CommonNames.DefaultStockManagerEmail];
        string[] roleNames = [CommonNames.AdminRole, CommonNames.SellerRole, CommonNames.ConsultantRole, CommonNames.StockManagerRole];
        for (int i = 0; i < emails.Length; i++)
        {
            var defaultUser = await userManager.Users
                .Where(x => x.UserName == emails[i])
                .SingleOrDefaultAsync();
            if (defaultUser == null)
            {
                var user = new AppUser { UserName = emails[i], Email = emails[i], EmailConfirmed = true };
                await userManager.CreateAsync(user, passwordsSection.GetValue<string>(roleNames[i])!);
                await userManager.AddToRoleAsync(user, roleNames[i]);
            }
        }
        return;
    }

    private async static Task EnsureMusicalInstrumentsAndTheirTypes(MusicalShopDbContext context)
    {
        if (context.MusicalInstruments.SingleOrDefault(mi => mi.GoodsId == Guid.Parse("05812ce5-61c0-4eaf-8580-aeeb653b2191")) == null)
        {
            var acousticGuitarType = new SpecificType { Name = "Акустическая гитара" };
            var drumsType = new SpecificType { Name = "Барабанная установка" };
            var fluteType = new SpecificType { Name = "Флейта" };
            var xylophoneType = new SpecificType { Name = "Ксилофон" };
            var synthesizerType = new SpecificType { Name = "Синтезатор" };
            var instruments = new List<MusicalInstrument>
        {
            new()
            {
                GoodsId = Guid.Parse("05812ce5-61c0-4eaf-8580-aeeb653b2191"),
                Description = "Акустическая гитара, с вырезом, санберст, Foix",
                Name = "FFG-3860C-SB",
                ReceiptDate = new DateTimeOffset(new DateTime(2023, 10, 12, 10, 20, 35)),
                Manufacturer = "John Spelberg",
                ManufacturerType = ManufacturerType.Master,
                Price = 9599,
                ReleaseYear = 2023,
                Status = GoodsStatus.InStock,
                Sale = Sales[0],
                Type = acousticGuitarType
            },
            new()
            {
                GoodsId = Guid.Parse("9384b7c1-6727-4dd0-88cc-7e1a1d9062cb"),
                Description = " Акустическая гитара, без выреза, без санберста, Hoix",
                Name = "Kolenval-SB-SUNBRESTLESS",
                ReceiptDate = new DateTimeOffset(new DateTime(2024, 10, 12, 10, 20, 35)),
                Manufacturer = "John Spelberg",
                ManufacturerType = ManufacturerType.Master,
                Price = 7499,
                ReleaseYear = 2023,
                Status = GoodsStatus.Reserved,
                Type = acousticGuitarType
            },
            new()
            {
                GoodsId = Guid.Parse("148b3083-f123-44df-8fce-af4c7014ac31"),
                Description = "Акустическая гитара, без порожков, Nice",
                Name = "L529-9X8823L-CCAS3-AR-2IC-3H-1SAEID50-BEZ-POROJKOF-OTVALILIS':(",
                ReceiptDate = new DateTimeOffset(new DateTime(2022, 1, 10, 1, 20, 35)),
#warning palindrome
                Manufacturer = "Завод гитар имени Инемиратигдова З.",
                ManufacturerType = ManufacturerType.Factory,
                Price = 3499,
                ReleaseYear = 2022,
                Status = GoodsStatus.InStock,
                Type = acousticGuitarType
            },
            new()
            {
                GoodsId = Guid.Parse("0e05ca0d-7e34-4b65-a5fc-3e7b69194390"),
                Description = "Акустическая гитара, без розетки, санберст отсутствует, полые порожки, струны из дерева, ж/б гриф, встроенная когтеточка, удобная лежанка и автокормушка с функцией будильника.",
                Name = "APPOLON-19-SUNBURSTLESS",
                ReceiptDate = null,
                Manufacturer = "Завод гитар для котов",
                ManufacturerType = ManufacturerType.Factory,
                Price = 2399,
                ReleaseYear = 2021,
                Status = GoodsStatus.AwaitingDelivery,
                Type = acousticGuitarType
            },
            new()
            {
                GoodsId = Guid.Parse("32eb21b6-6d88-42aa-9326-114297689a59"),
                Description = "Акустическая гитара, без выреза, артишок, Belucci",
                Name = "LDPWD",
                ReceiptDate = null,
                Manufacturer = "Завод собачьих гитар",
                ManufacturerType = ManufacturerType.Factory,
                Price = 2399,
                ReleaseYear = 2020,
                Status = GoodsStatus.AwaitingDelivery,
                Type = acousticGuitarType
            },
            new()
            {
                GoodsId = Guid.Parse("6f5c6af2-6fb7-4cf2-8730-5e365d2c1032"),
                Description = "Акустическая гитара, без выреза, но с вырезом, проивзедено в США",
                Name = "MyFirstGuitar",
                ReceiptDate = new DateTimeOffset(new DateTime(2022, 1, 10, 1, 20, 35)),
                Manufacturer = "John Maloe",
                ManufacturerType = ManufacturerType.Master,
                Price = 88990,
                ReleaseYear = 2024,
                Status = GoodsStatus.InStock,
                Type = acousticGuitarType
            },
            new()
            {
                GoodsId = Guid.Parse("2b14e3ec-6af4-4094-ba4b-255933603cc9"),
                Description = "*Барабанная дробь*... Барабанная установка \"Барабанная мечта\" - барабанный рай барабанного любителя.",
                Name = "Стукач",
                ReceiptDate = new DateTimeOffset(new DateTime(2024, 1, 10, 1, 20, 35)),
                Manufacturer = "Барабанный лидер",
                ManufacturerType = ManufacturerType.Factory,
                Price = 14900,
                ReleaseYear = 2019,
                Status = GoodsStatus.AwaitingDelivery,
                Type = drumsType
            },
            new()
            {
                GoodsId = Guid.Parse("a07319f6-4944-4f06-bb1c-b77c27e73b1d"),
                Description = "Многослойные барабаны позволят слышать себя непревзойденно.",
                Name = "Knocker-knocker",
                ReceiptDate = new DateTimeOffset(new DateTime(2024, 1, 10, 1, 20, 35)),
                Manufacturer = "Барабань-ка",
                ManufacturerType = ManufacturerType.Factory,
                Price = 1900,
                ReleaseYear = 2019,
                Status = GoodsStatus.InStock,
                Type = drumsType
            },
            new()
            {
                GoodsId = Guid.Parse("ee1c5679-2018-4192-8e02-6efed0ef8c5a"),
                Description = "Народный духовой инструмент",
                Name = "[oOo]",
                ReceiptDate = new DateTimeOffset(new DateTime(2024, 1, 10, 1, 20, 35)),
                Manufacturer = "Завод барабанных флейт имени Дыхалова",
                ManufacturerType = ManufacturerType.Factory,
                Price = 299,
                ReleaseYear = 2023,
                Status = GoodsStatus.InStock,
                Sale = Sales[0],
                Type = fluteType
            },
            new()
            {
                GoodsId = Guid.Parse("9d7e4b3b-cbaa-4327-af1c-1ea3e232d68a"),
                Description = "Компактное пианино, 3 режима, подсветка, присутствует нейросеть, позволяющая схватывать колебания головного мозга с целью воспроизведения желаемой мелодии. Сделано в СССР",
                Name = "Sntzr-1937",
                ReceiptDate = new DateTimeOffset(new DateTime(2023, 1, 10, 1, 20, 35)),
                Manufacturer = "Steve Pianoe",
                ManufacturerType = ManufacturerType.Master,
                Price = 11590,
                ReleaseYear = 1975,
                Status = GoodsStatus.InStock,
                Type = synthesizerType
            },
            new()
            {
                GoodsId = Guid.Parse("d7b2ff21-cc80-41fa-bef5-3ca93c5ec4fa"),
                Description = "Данный синтезатор изготовлен из нержавеющего пластика, слоновьего зуба и экранированного хлеба. Корпус выполнен в командной строке.",
                Name = "Bearded?Bear?Beer?Breed?Bread?",
                ReceiptDate = new DateTimeOffset(new DateTime(2023, 1, 10, 1, 20, 35)),
                Manufacturer = "Синтезаторы? Производим.",
                ManufacturerType = ManufacturerType.Factory,
                Price = 2390,
                ReleaseYear = 2022,
                Status = GoodsStatus.AwaitingDelivery,
                Sale = Sales[3],
                Type = synthesizerType
            },
        };
            //context.Add(acousticGuitarType);
            //context.Add(drumsType);
            //context.Add(fluteType);
            //context.Add(xylophoneType);
            //context.Add(synthesizerType);
            context.AddRange(instruments);
            context.SaveChanges();
        }
    }

    private async static Task EnsureAccessoriesAndTheirTypes(MusicalShopDbContext context)
    {
        if (context.Accessories.SingleOrDefault(mi => mi.GoodsId == Guid.Parse("05812ce5-61c0-4eaf-1937-aeeb653b2191")) == null)
        {
            var chairType = new SpecificType { Name = "Табуретка регулируемая" };
            var keychainType = new SpecificType { Name = "Брелок" };
            var accessories = new List<Accessory>
            {
                new()
                {
                    GoodsId = Guid.Parse("05812ce5-61c0-4eaf-1937-aeeb653b2191"),
                    Description = "Круглая табуретка",
                    Name = "Табуретка",
                    ReceiptDate = new DateTimeOffset(new DateTime(2023, 10, 12, 10, 20, 35)),
                    Price = 599,
                    Status = GoodsStatus.InStock,
                    Type = chairType,
                    Color = "Прозрачный",
                    Sale = Sales[0],
                    Size = "регулировка высоты от 10 до 150 см, 50см радиус седла"
                },
                new()
                {
                    GoodsId = Guid.Parse("05812ce5-61c0-4eaf-1938-aeeb653b2191"),
                    Description = "Брелок с граммофоном отлично смотрится на архивных вещах",
                    Name = "Брелок с граммофоном",
                    ReceiptDate = new DateTimeOffset(new DateTime(2023, 10, 12, 10, 20, 35)),
                    Price = 99,
                    Status = GoodsStatus.Reserved,
                    Type = keychainType,
                    Sale = Sales[3],
                    Color = "Черно-желтый",
                    Size = "20см x 0.5см x 3см"
                },
                new()
                {
                    GoodsId = Guid.Parse("05812ce5-61c0-4eaf-1939-aeeb653b2191"),
                    Description = "Набор 3 в 1: пюпитр и каподастр",
                    Name = "Стартующий гитарист",
                    ReceiptDate = new DateTimeOffset(new DateTime(2023, 10, 12, 10, 20, 35)),
                    Price = 699,
                    Status = GoodsStatus.Sold,
                    Type = keychainType,
                    Sale = Sales[4],
                    Color = "Черно-рыжий",
                    Size = "Высота пюпитра: 30-200см. Каподастр 13см x 1см x 12 см"
                },
            };
            context.AddRange(accessories);
            context.SaveChanges();
        }
    }

    private static async Task EnsureAudioEquipmentUnitsAndTheirTypes(MusicalShopDbContext context)
    {
#warning not implemented
        return;
    }

    private static async Task EnsureSheetMusicEditionsAndTheirTypes(MusicalShopDbContext context)
    {
#warning not implemented
        return;
    }
}