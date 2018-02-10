namespace RotS.LineParser.Arken {

	#region Directives
	using System;
	using System.Runtime.InteropServices;
	using RotS.LineParser.Core;

	#endregion

	[ClassInterface(ClassInterfaceType.AutoDual)]
	[ComVisible(true)]
	[Guid("53C47A57-DB70-44FC-983D-1C9E0F24DB7A")]
	[ProgId("RotS.LineParser.PebbleslideParser")]
	public class ArkenParser
		: LineParser {

		/// <summary>
		/// Initializes a new instance of the <see cref="ArkenParser"/> class.
		/// </summary>
		public ArkenParser() {
			throw new NotImplementedException(@"The arken parser is not currenty implemented.");
		}

		/// <summary>
		/// Called when the <seealso cref="T:RotS.LineParser.Core.LineParser" /> has completed preliminary initialization.
		/// </summary>
		protected override void OnInitialize() {
			base.OnInitialize();
			throw new NotImplementedException();
		}

	}

}
