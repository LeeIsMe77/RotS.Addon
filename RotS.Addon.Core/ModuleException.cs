namespace RotS.Addon.Core {

	#region Directives
	using System;
	using RotS.Addon.Core;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	#endregion

	public class ModuleException
		: Exception {

		#region Static

		/// <summary>
		/// Logs the exception.
		/// </summary>
		/// <param name="caught">The caught.</param>
		public static void LogException(JmcModule module, Exception caught) {
			// TODO: Implement LogException.
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ModuleException"/> class.
		/// </summary>
		/// <param name="innerException">The inner exception.</param>
		/// <param name="message">The message.</param>
		public ModuleException(Exception innerException, string message)
			: base(message, innerException) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="ModuleException"/> class.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		public ModuleException(string message, Exception innerException)
			: base(message, innerException) { }

		#endregion

	}

}
