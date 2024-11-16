using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common;
public class Cmd
{
    public const string MysqldumpArguments = "-u{0} -p{1} -h {2} --ignore-table={3}.__efmigrationshistory --ignore-table={3}.aspnetroleclaims --ignore-table={3}.aspnetroles --ignore-table={3}.aspnetuserclaims --ignore-table={3}.aspnetuserlogins --ignore-table={3}.aspnetuserroles --ignore-table={3}.aspnetusers --ignore-table={3}.aspnetusertokens --add-drop-table --routines --databases {3} --result-file=\"{4}\"";
    public const string MysqRestoreBackupArgs = "-u{0} -p{1} --execute=\"{2}\"";
}
