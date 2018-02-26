namespace RotS.Addon.Core.Extensions {

	#region Directives
	using System.Data.Common;

	#endregion

	/// <summary>
	/// A set of commonly used extensions methods for the <seealso cref="DbConnectionStringBuilder"/> type.
	/// </summary>
	public static class DbConnectionStringBuilderExtension {

		/// <summary>
		/// Creates a <seealso cref="DbConnection"/> of a type specific to that of the connection string builder.
		/// </summary>
		/// <param name="connectionStringBuilder">The connection string builder.</param>
		/// <returns>DbConnection.</returns>
		public static DbConnection CreateConnection(this DbConnectionStringBuilder connectionStringBuilder) {
			DbConnection connection = null;
			if (connectionStringBuilder != null) {
				connection = DbProviderFactories.GetFactory(connectionStringBuilder.GetType().Namespace).CreateConnection();
				connection.ConnectionString = connectionStringBuilder.ConnectionString;
			}
			return connection;
		}

	}

}
