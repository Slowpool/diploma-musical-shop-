namespace Common;

// TODO it should be done via localization
public static class CommonNames
{
    public const string DefaultAdminEmail = "admin@koshka.prosrochka";
    public const string DefaultSellerEmail = "seller@koshka.prosrochka";
    public const string DefaultConsultantEmail = "consultant@koshka.prosrochka";
    public const string DefaultStockManagerEmail = "stockmanager@koshka.prosrochka";
    public const string DefaultSuperuserEmail = "superuser@koshka.prosrochka";

    public const string Admin = "Администратор";
    public const string Seller = "Продавец";
    public const string Consultant = "Консультант";
    public const string StockManager = "Менеджер складского учета";
    public const string Superuser = "Суперпользователь";

    public const string NotExistingGuid = "";

    // TODO now it is a wild savage workaround
    public const string GoodsIdsInCartKey = "cart-with-goods";
	public const char GoodsIdSeparator = ',';
	public const char GoodsIdAndKindSeparator = '|';

    public const string MusicalInstruments = "Музыкальные инструменты";
    public const string AudioEquipmentUnits = "Аудиооборудование";
    public const string Accessories = "Аксессуары";
    public const string SheetMusicEditions = "Нотные издания";

    public const string FieldIsRequiredMessageRu = "Значение поля \"{0}\" не может быть пустым";
    public const string MaxLengthViolationMessageRu = "Значение поля \"{0}\" не может быть длиннее {1} символов";
    public const string HtmlTagsViolationMessageRu = "Значение поля \"{0}\" не может содержать HTML-теги";

    public const string GeneralReportType = "Общий";
    public const string SpecificGoodsReportType = "Конкретный";

    public const string NumberOfSalesReportSubtype = "Количество продаж";
    public const string AverageSalesSpeedReportSubtype = "Средняя скорость продаж";
    public const string SalesRevenueReportSubtype = "Доход с продаж";

    public const string ReportAbsoluteChart = "Абсолютная";
    public const string ReportRelativeChart = "Относительная";
    public const string ReportAbsoluteAndRelativeChart = "Абсолютная и относительная";
}
