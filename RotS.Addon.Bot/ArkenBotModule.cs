namespace RotS.Addon.Bot {

	#region Directives
	using System;
	using System.Runtime.InteropServices;
	using RotS.Addon.Core;
	#endregion

	[ClassInterface(ClassInterfaceType.AutoDual)]
	[ComVisible(true)]
	[Guid("0FF1B455-1080-42F1-A1C0-25E9AE034CD2")]
	[ProgId("RotS.Addon.ArkenBotModule")]
	public class ArkenBotModule
		: JmcModule {

		/// <summary>
		/// Initializes a new instance of the <see cref="ArkenBotModule"/> class.
		/// </summary>
		public ArkenBotModule() {
			throw new NotImplementedException(@"The arken parser is not currently implemented.");
		}

		/// <summary>
		/// Called when the <seealso cref="T:RotS.Addon.Core.Module" /> has completed preliminary initialization.
		/// </summary>
		protected override void OnInitialize() {
			base.OnInitialize();
			throw new NotImplementedException();
		}

	}

}
