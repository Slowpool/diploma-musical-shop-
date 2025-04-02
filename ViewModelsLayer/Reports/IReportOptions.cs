using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelsLayer.Reports;
public interface IReportOptions
{
    public DateTime? FromDate { get; init; }
    public DateTime? ToDate { get; init; }
    public ReportSubtype Subtype { get; init; }
}
