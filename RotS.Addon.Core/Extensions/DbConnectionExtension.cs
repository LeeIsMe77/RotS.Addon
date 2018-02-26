namespace RotS.Addon.Core.Extensions {

	#region Directives
	using System.Data;
	using System.Data.Common;

	#endregion

	/// <summary>
	/// A set of commonly used extensions methods for the <seealso cref="DbConnection"/> type.
	/// </summary>
	public static class DbConnectionExtension {

		/// <summary>
		/// Creates a data adapter of a type specific to that of the connection
		/// </summary>
		/// <param name="thisConnection">The connection to generate the <see cref="DbDataAdapter" /> for.</param>
		/// <returns>DbDataAdapter.</returns>
		public static DbDataAdapter CreateDataAdapter(this IDbConnection thisConnection) {
			return thisConnection == null ? null : DbProviderFactories.GetFactory(thisConnection.GetType().Namespace).CreateDataAdapter();
		}

	}

}
