namespace RotS.LineParser.Core.Extensions {

	#region Directives
	using RotS.LineParser.Core.Common;
	using TTCOREEXLib;
	#endregion

	/// <summary>
	/// A set of commonly used extensions methods for the <seealso cref="JmcObj"/> type.
	/// </summary>
	public static class JmcObjectExtension {

		/// <summary>
		/// Navigates the specified directions.
		/// </summary>
		/// <param name="jmcObject">The JMC object.</param>
		/// <param name="directions">The directions.</param>
		public static void Navigate(this JmcObj jmcObject, params Direction[] directions) {
			if (jmcObject == null) {
				return;
			}

			foreach (var direction in directions) {
				jmcObject.Send(direction.ToString());
			}
		}

	}

}
