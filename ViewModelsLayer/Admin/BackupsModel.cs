using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelsLayer.Admin;
public record class BackupsModel(Dictionary<DateTime, string> BackupsList);
