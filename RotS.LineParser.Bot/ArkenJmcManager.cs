namespace RotS.LineParser.Bot {

	#region Directives
	using System;
	using System.Runtime.InteropServices;
	using RotS.LineParser.Core;

	#endregion

	[ClassInterface(ClassInterfaceType.AutoDual)]
	[ComVisible(true)]
	[Guid("0FF1B455-1080-42F1-A1C0-25E9AE034CD2")]
	[ProgId("RotS.LineParser.ArkenJmcManager")]
	public class ArkenJmcManager
		: JmcManager {

		/// <summary>
		/// Initializes a new instance of the <see cref="ArkenJmcManager"/> class.
		/// </summary>
		public ArkenJmcManager() {
			throw new NotImplementedException(@"The arken parser is not currently implemented.");
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
