namespace RotS.Addon.Toggle {

	#region Directives
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Runtime.InteropServices;
	using System.Xml.Linq;
	using RotS.Addon.Core;
	using RotS.Addon.Toggle.Common;
	#endregion

	[ClassInterface(ClassInterfaceType.AutoDual)]
	[ComVisible(true)]
	[Guid("BEBD62EB-B3FF-4AA9-ADB4-9B82372A2FCE")]
	[ProgId("RotS.Addon.ToggleModule")]
	public class ToggleModule
		: JmcModule {

		#region Properties

		private readonly object _processQueueLock = new object();

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

		/// <summary>
		/// Called when the <seealso cref="JmcModule" /> has completed preliminary initialization.
		/// </summary>
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
		}

		/// <summary>
		/// Called when an incoming line has been received from the server.
		/// </summary>
		/// <param name="incomingLine">The incoming line.</param>
		protected override void OnIncoming(string incomingLine) {
			base.OnIncoming(incomingLine);
			// If this line parser is enabled....
			if (this.Enabled) {
				// Create a queue to hold all of the toggles....
				var processQueue = new Queue<Toggle>();
				// ...and loop through each toggle in parallel...
				this.ToggleCollection.AsParallel().ForAll(toggle => {
					// ...and if the toggle is enabled and is a RegEx match for the line that has been received....
					if (toggle.Enabled && toggle.IsMatch(incomingLine)) {
						// ..lock the lock object due to the queue not being thread-safe....
						lock (_processQueueLock) {
							// ...and enqueue the toggle into the queue.
							processQueue.Enqueue(toggle);
						}
					}
				});
				// While the process queue contains toggles...
				while (processQueue.Count > 0) {
					// ...process them one-at-a-time.
					processQueue.Dequeue().Process(incomingLine);
				}

			}

		}

		#endregion

	}

}
