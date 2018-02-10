
namespace RotS.LineParser.Core.Common {

	#region Directives
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Runtime.InteropServices;
	using System.Text;
	using System.Threading.Tasks;

	#endregion

	[ComVisible(true)]
	public enum DatabaseType {
		None = 0,
		SQLServer = 1,
		ODBC = 2,
		OleDB = 3,
	}

}
