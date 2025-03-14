using DataLayer.Models;
using DataLayer.Models.SpecificTypes;
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
            LocalSaleDate = new DateTime(2023, 10, 1, 10, 49, 07),
            PaidBy = SalePaidBy.BankCard,
            IsPaid = true
        },
        new()
        {
            SaleId = Guid.Parse("0d9ee495-b44c-4465-ba45-6fa6ff562579"),
            Status = SaleStatus.Sold,
            LocalSaleDate = new DateTime(2023, 1, 19, 17, 19, 11),
            PaidBy = SalePaidBy.Cash,
            IsPaid = true
        },
        new()
        {
            SaleId = Guid.Parse("e4c58ecd-e871-4579-b7bc-58563b67299a"),
            Status = SaleStatus.Sold,
            LocalSaleDate = new DateTime(2023, 5, 28, 15, 50, 01),
            PaidBy = SalePaidBy.BankCard,
            IsPaid = true
        },
        new()
        {
            SaleId = Guid.Parse("286b3185-c983-4339-86c9-b12fc8fac5e2"),
            Status = SaleStatus.Returned,
            LocalSaleDate = new DateTime(2023, 3, 1, 13, 20, 35),
            PaidBy = SalePaidBy.Cash,
            IsPaid = true
        },
        new()
        {
            SaleId = Guid.Parse("bf1cf858-f491-4234-9331-0b4abef9f0e8"),
            Status = SaleStatus.Reserved,
            LocalSaleDate = new DateTime(2023, 7, 9, 10, 11, 35),
            PaidBy = SalePaidBy.BankCard,
            IsPaid = true
        },
        ];

#warning seed another data like types of musical instruments like guitars
    public static async Task SeedAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await EnsureRolesAsync(roleManager);

        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        await EnsureUsersAsync(userManager, services);
        // Unneccesary stuff for app, but it provides visibility for test-review
        // TODO remove everything below from production
        var context = services.GetRequiredService<MusicalShopDbContext>();
        await EnsureMusicalInstrumentsAndTheirTypes(context);
        await EnsureAccessoriesAndTheirTypes(context);
        await EnsureAudioEquipmentUnitsAndTheirTypes(context);
        await EnsureSheetMusicEditionsAndTheirTypes(context);
    }

    private async static Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        string[] roles = { CommonNames.Admin, CommonNames.Seller, CommonNames.Consultant, CommonNames.StockManager };
        for (int i = 0; i < 4; i++)
        {
            if (!await roleManager.RoleExistsAsync(roles[i]))
            {
                var role = new IdentityRole { Name = roles[i] };
                await roleManager.CreateAsync(role);
            }
        }
    }

    private async static Task EnsureUsersAsync(UserManager<IdentityUser> userManager, IServiceProvider services)
    {
        var configuration = services.GetRequiredService<IConfiguration>();
        var passwordsSection = configuration.GetSection("DefaultPasswords");
#warning it could have been implemented more, more simply.
        string[] emails = [CommonNames.DefaultAdminEmail, CommonNames.DefaultSellerEmail, CommonNames.DefaultConsultantEmail, CommonNames.DefaultStockManagerEmail];
        string[] roleNames = [CommonNames.Admin, CommonNames.Seller, CommonNames.Consultant, CommonNames.StockManager];
        for (int i = 0; i < emails.Length; i++)
        {
            var defaultUser = await userManager.Users
                .Where(x => x.UserName == emails[i])
                .SingleOrDefaultAsync();
            if (defaultUser == null)
            {
                var user = new IdentityUser { UserName = emails[i], Email = emails[i], EmailConfirmed = true };
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
            var acousticGuitarType = new MusicalInstrumentSpecificType { Name = "Акустическая гитара" };
            var drumsType = new MusicalInstrumentSpecificType { Name = "Барабанная установка" };
            var fluteType = new MusicalInstrumentSpecificType { Name = "Флейта" };
            var xylophoneType = new MusicalInstrumentSpecificType { Name = "Ксилофон" };
            var synthesizerType = new MusicalInstrumentSpecificType { Name = "Синтезатор" };
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
                SpecificType = acousticGuitarType
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
                SpecificType = acousticGuitarType
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
                SpecificType = acousticGuitarType
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
                SpecificType = acousticGuitarType
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
                SpecificType = acousticGuitarType
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
                SpecificType = acousticGuitarType
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
                SpecificType = drumsType
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
                SpecificType = drumsType
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
                Status = GoodsStatus.Sold,
                Sales = [Sales[0]],
                SpecificType = fluteType
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
                SpecificType = synthesizerType
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
                Status = GoodsStatus.Sold,
                Sales = [Sales[3]],
                SpecificType = synthesizerType
            },
        };
            context.AddRange(instruments);
            context.SaveChanges();
        }
    }

    private async static Task EnsureAccessoriesAndTheirTypes(MusicalShopDbContext context)
    {
        if (context.Accessories.SingleOrDefault(mi => mi.GoodsId == Guid.Parse("05812ce5-61c0-4eaf-1937-aeeb653b2191")) == null)
        {
            var chairType = new AccessorySpecificType { Name = "Табуретка регулируемая" };
            var keychainType = new AccessorySpecificType { Name = "Брелок" };
            var accessories = new List<Accessory>
            {
                new()
                {
                    GoodsId = Guid.Parse("05812ce5-61c0-4eaf-1937-aeeb653b2191"),
                    Description = "Круглая табуретка",
                    Name = "Табуретка",
                    ReceiptDate = new DateTimeOffset(new DateTime(2023, 10, 12, 10, 20, 35)),
                    Price = 599,
                    Status = GoodsStatus.Sold,
                    SpecificType = chairType,
                    Color = "Прозрачный",
                    Sales = [Sales[0]],
                    Size = "регулировка высоты от 10 до 150 см, 50см радиус седла"
                },
                new()
                {
                    GoodsId = Guid.Parse("bf73bc1d-5d82-460b-9cf4-cd08e117face"),
                    Description = "Брелок с граммофоном отлично смотрится на архивных вещах",
                    Name = "Брелок с граммофоном",
                    ReceiptDate = new DateTimeOffset(new DateTime(2023, 10, 12, 10, 20, 35)),
                    Price = 99,
                    Status = GoodsStatus.Sold,
                    SpecificType = keychainType,
                    Sales = [Sales[3]],
                    Color = "Черно-желтый",
                    Size = "20см x 0.5см x 3см"
                },
                new()
                {
                    GoodsId = Guid.Parse("6d4e31a0-9809-44c8-810f-7e0c4f435e03"),
                    Description = "Набор 3 в 1: пюпитр и каподастр",
                    Name = "Стартующий гитарист",
                    ReceiptDate = new DateTimeOffset(new DateTime(2023, 10, 12, 10, 20, 35)),
                    Price = 699,
                    Status = GoodsStatus.Sold,
                    SpecificType = keychainType,
                    Sales = [Sales[4]],
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