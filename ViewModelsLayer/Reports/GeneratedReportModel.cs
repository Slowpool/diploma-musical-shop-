using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelsLayer.Reports;
public record GeneratedReportModel(List<ReportModelItem> ReportItems, ReportCommonOptionsDto Options);
