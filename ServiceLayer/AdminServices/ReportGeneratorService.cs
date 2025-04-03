using BizLogicBase.Validation;
using DataLayer.NotMapped;
using DataLayer.SupportClasses;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.GoodsServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsLayer.Reports;
using ViewModelsLayer.Reports.ExtensionMethods;

namespace ServiceLayer.AdminServices;

public interface IReportGeneratorService : IErrorAdder
{
    Task<List<ReportModelItem>> Generate(ReportCommonOptions options);
    // TODO probably it should be hidden
    Task<List<ReportModelItem>> GenerateGeneralReport(GeneralReportOptions options);
    Task<List<ReportModelItem>> GenerateSpecificReport(SpecificReportOptions options);
}

public class ReportGeneratorService(IMapKindOfGoodsService kindOfGoodsMapper) : ErrorAdder, IReportGeneratorService
{
    public async Task<List<ReportModelItem>> Generate(ReportCommonOptions options)
    {
        return options.Type switch
        {
            ReportType.General => await GenerateGeneralReport(new GeneralReportOptions(options)),
            ReportType.SpecificGoods => await GenerateSpecificReport(new SpecificReportOptions(options)),
            _ => throw new Exception()
        };
    }

    public async Task<List<ReportModelItem>> GenerateGeneralReport(GeneralReportOptions options)
    {
        List<ReportModelItem> models = [];
        foreach(var kindOfGoods in options.KindsOfGoods)
        {
            int number = await kindOfGoodsMapper.MapToSpecificGoods(kindOfGoods)
                                                .Where(options.FromDate, options.ToDate)
                                                .Select(options.Subtype)
                                                ;
            models.Add(new(kindOfGoodsMapper.MapToString(kindOfGoods), number));
        }
        return models;
    }

    public async Task<List<ReportModelItem>> GenerateSpecificReport(SpecificReportOptions options)
    {

        return [];
    }
}
