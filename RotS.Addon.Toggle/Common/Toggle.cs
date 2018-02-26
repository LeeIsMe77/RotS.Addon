namespace RotS.Addon.Toggle.Common {

	#region Directives
	using System.Runtime.InteropServices;
	using System.Text.RegularExpressions;
	using System.Xml.Linq;
	using RotS.Addon.Core.Extensions;
	#endregion

	/// <summary>
	/// A class encapsulating logic required for all modules as used by the <seealso cref="TTCOREEXLib.JmcObj"/>.
	/// </summary>
	public abstract class Toggle {

		#region Properties

		#region Enabled

		/// <summary>
		/// Gets a value indicating whether this <see cref="Toggle"/> is enabled.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		[ComVisible(true)]
		public bool Enabled { get; private set; }

		#endregion

		#region IgnoreCase

		/// <summary>
		/// Gets a value indicating whether case should be ignored on the match pattern.
		/// </summary>
		/// <value><c>true</c> if case is ignored; otherwise, <c>false</c>.</value>
		[ComVisible(true)]
		public virtual bool IgnoreCase { get; } = true;

		#endregion

		#region MatchString

		/// <summary>
		/// Gets the <seealso cref="Regex"/> match pattern used to determine if the <seealso cref="Toggle"/> should continue processing.
		/// </summary>
		/// <value>The match pattern.</value>
		[ComVisible(true)]
		public abstract string MatchPattern { get; }

		#endregion

		#region ModuleCollection

		/// <summary>
		/// Gets the module collection.
		/// </summary>
		/// <value>The module collection.</value>
		public ToggleCollection ToggleCollection { get; }

		#endregion

		#region ModuleName

		/// <summary>
		/// Gets the name of the module.
		/// </summary>
		/// <value>The name of the module.</value>
		[ComVisible(true)]
		public abstract string ToggleName { get; }

		#endregion

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="Toggle" /> class.
		/// </summary>
		/// <param name="moduleCollection">The module collection.</param>
		public Toggle(ToggleCollection moduleCollection) {
			this.ToggleCollection = moduleCollection;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Enables the matching and processing of this <seealso cref="Toggle"/>.
		/// </summary>
		[ComVisible(true)]
		public void Enable() {
			try {
				this.Enabled = true;
				this.OnEnable();
			}
			catch { }
		}

		/// <summary>
		/// Disables the matching and processing of this <seealso cref="Toggle"/>.
		/// </summary>
		[ComVisible(true)]
		public void Disable() {
			try {
				this.Enabled = false;
				this.OnDisable();
			}
			catch { }
		}

		/// <summary>
		/// Initializes this instance of the <seealso cref="Toggle"/> by loading custom properties from the configuration.
		/// </summary>
		/// <param name="modulesConfiguration">The configuration.</param>
		public void Initialize(XElement modulesConfiguration) {
			var moduleConfiguration = modulesConfiguration?.Element(this.ToggleName);
			if (moduleConfiguration != null) {
				this.Enabled = moduleConfiguration.SafeAttributeValue<bool>(nameof(Toggle.Enabled), false);
				this.ToggleCollection.ToggleModule.JmcObject.ShowMe($@"Module {this.ToggleName} is {(this.Enabled ? @"Enabled" : @"Disabled")}");
				this.OnInitialize(moduleConfiguration);
			}
		}

		/// <summary>
		/// Determines whether the specified search string is match to the provided match pattern.
		/// </summary>
		/// <param name="searchString">The search string.</param>
		/// <returns><c>true</c> if the specified search string is match; otherwise, <c>false</c>.</returns>
		internal bool IsMatch(string searchString) {
			return Regex.IsMatch(searchString, this.MatchPattern, this.IgnoreCase ? RegexOptions.IgnoreCase : RegexOptions.None);
		}

		/// <summary>
		/// Executes custom processing of the line received from the client after a match has been determined.
		/// </summary>
		/// <param name="incomingLine">The incoming line.</param>
		internal void Process(string incomingLine) {
			try {
				this.OnProcess(incomingLine);
			}
			catch { }
		}

		/// <summary>
		/// Saves custom property settings to the configuration element.
		/// </summary>
		/// <returns>XElement.</returns>
		internal XElement SaveConfiguration() {
			var configuration = new XElement(this.ToggleName);
			try {
				this.ToggleCollection.ToggleModule.JmcObject.ShowMe($@"Module {this.ToggleName} is {(this.Enabled ? @"Enabled" : @"Disabled")}");
				configuration.Add(new XAttribute(nameof(Toggle.Enabled), this.Enabled));
				this.OnConfigurationSaved(configuration);
			}
			catch { }
			return configuration;
		}

		#endregion

		#region Cascaded Methods

		/// <summary>
		/// Called when the initial configuration settings have been saved and the implementing class must save custom property settings.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		protected virtual void OnConfigurationSaved(XElement configuration) { }

		/// <summary>
		/// Called when the implementing class must perform custom logic in order to enable itself.
		/// </summary>
		protected virtual void OnEnable() { }

		/// <summary>
		/// Called when the implementing class must perform custom logic to disable itself.
		/// </summary>
		protected virtual void OnDisable() { }

		/// <summary>
		/// Called when the implementing class must initialize custom property settings from the configuration.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		protected virtual void OnInitialize(XElement configuration) { }

		/// <summary>
		/// Called when the implementing class must provide custom processing code after a match has been determined.
		/// </summary>
		/// <param name="incomingLine">The incoming line.</param>
		protected abstract void OnProcess(string incomingLine);

		#endregion

	}

}
