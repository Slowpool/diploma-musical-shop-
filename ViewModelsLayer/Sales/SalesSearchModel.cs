using DataLayer.SupportClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelsLayer.Sales;
public record class SalesSearchModel(string ResearchText, List<SaleSearchModel> Sales, int ResultsCount, SalesFilterOptions Filter, SalesOrderByOptions OrderBy);
