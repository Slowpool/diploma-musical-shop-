using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.SupportClasses;
public interface ISoftDeletable
{
    public bool SoftDeleted { get; set; }
}
