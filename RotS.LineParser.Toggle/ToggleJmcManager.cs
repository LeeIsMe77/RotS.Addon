namespace RotS.LineParser.Toggle {

	#region Directives
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Runtime.InteropServices;
	using System.Text;
	using System.Threading.Tasks;
	using System.Xml.Linq;
	using RotS.LineParser.Core;
	using RotS.LineParser.Toggle.Common;

	#endregion

	[ClassInterface(ClassInterfaceType.AutoDual)]
	[ComVisible(true)]
	[Guid("BEBD62EB-B3FF-4AA9-ADB4-9B82372A2FCE")]
	[ProgId("RotS.LineParser.ToggleJmcManager")]
	public class ToggleJmcManager
		: JmcManager {

		#region Properties

		#region ToggleCollection

		private ToggleCollection _toggleCollection;

		/// <summary>
		/// Gets the toggle collection.
		/// </summary>
		/// <value>The toggle collection.</value>
		public ToggleCollection ToggleCollection {
			get {
				if (_toggleCollection == null) {
					_toggleCollection = new ToggleCollection(this);
					_toggleCollection.AddRange(new Toggle[] {
						new DrinkToggle(_toggleCollection),
						new FoodToggle(_toggleCollection)
					});
				}
				return _toggleCollection;
			}
		}

		#endregion

		#endregion

		#region Method Overrides

		protected override void OnInitialize() {
			base.OnInitialize();
		}

		/// <summary>
		/// Called when the configuration settings must be loaded.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		protected override void OnConfigurationSettingsLoaded(XElement configuration) {
			base.OnConfigurationSettingsLoaded(configuration);
			if (configuration != null) {
				foreach (var toggle in this.ToggleCollection) {
					toggle.Initialize(configuration);
				}
			}
		}

		/// <summary>
		/// Called when the configuration settings must be saved.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		protected override void OnConfigurationSettingsSaved(XElement configuration) {
			base.OnConfigurationSettingsSaved(configuration);
			configuration.Add(this.ToggleCollection.Select(toggle => toggle.SaveConfiguration()));
			this.Log(configuration.ToString(), @"normal");
		}

		/// <summary>
		/// Called when an incoming line has been received from the server.
		/// </summary>
		/// <param name="incomingLine">The incoming line.</param>
		protected override void OnIncoming(string incomingLine) {
			base.OnIncoming(incomingLine);
		}

		#endregion

	}

}
