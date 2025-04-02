using BizLogicBase.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.AdminServices;

public interface IReportGeneratorService : IErrorAdder
{
    Task<>
}

public class ReportGeneratorService : ErrorAdder, IReportGeneratorService
{

}
