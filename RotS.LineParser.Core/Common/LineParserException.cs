namespace RotS.LineParser {

	#region Directives
	using System;
	using RotS.LineParser.Core;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	#endregion

	public class LineParserException
		: Exception {

		#region Static

		/// <summary>
		/// Logs the exception.
		/// </summary>
		/// <param name="caught">The caught.</param>
		public static void LogException(JmcManager lineParser, Exception caught) {
			// TODO: Implement LogException.
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="LineParserException"/> class.
		/// </summary>
		/// <param name="innerException">The inner exception.</param>
		/// <param name="message">The message.</param>
		public LineParserException(Exception innerException, string message)
			: base(message, innerException) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="LineParserException"/> class.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		public LineParserException(string message, Exception innerException)
			: base(message, innerException) { }

		#endregion

	}

}
