using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common;

public class Consts
{
    public const string BackupDateTimeFormat = "yyyy_MM_dd HH_mm_ss";

    public const int GoodsDescriptionMaxLength = 500;
    public const int GoodsNameMaxLength = 100;
    public const int GoodsPriceMinValue = 1;
    public const int GoodsPriceMaxValue = int.MaxValue;
    public const int GoodsSpecificTypeMaxLength = 100;

    public const int AccessoryColorMaxLength = 100;
    public const int AccessorySizeMaxLength = 200;

    public const int SheetMusicEditionAuthorMaxLength = 100;

    public const int MusicalInstrumentManufacturerMaxLength = 100;

    public const int DEFAULT_PAGING_SIZE = 15;
    public const int DEFAULT_PAGING_NUMBER = 1;
}
