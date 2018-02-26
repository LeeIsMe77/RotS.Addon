namespace RotS.Addon.Core.Common {

	#region Directives
	using System.Runtime.InteropServices;
	#endregion

	[ComVisible(true)]
	public enum DatabaseType {
		None = 0,
		SQLServer = 1,
		ODBC = 2,
		OleDB = 3,
	}

}
