namespace RotS.LineParser.Toggle.Common {

	#region Directives
	using System.Runtime.InteropServices;
	using System.Text.RegularExpressions;
	using System.Xml.Linq;
	using RotS.LineParser.Core.Extensions;
	#endregion

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
		/// Gets a value indicating whether [ignore case].
		/// </summary>
		/// <value><c>true</c> if [ignore case]; otherwise, <c>false</c>.</value>
		[ComVisible(true)]
		public virtual bool IgnoreCase { get; } = true;

		#endregion

		#region MatchString

		/// <summary>
		/// Gets the match pattern.
		/// </summary>
		/// <value>The match pattern.</value>
		[ComVisible(true)]
		public abstract string MatchPattern { get; }

		#endregion

		#region ToggleCollection

		/// <summary>
		/// Gets the toggle collection.
		/// </summary>
		/// <value>The toggle collection.</value>
		public ToggleCollection ToggleCollection { get; }

		#endregion

		#region ToggleName

		/// <summary>
		/// Gets the name of the toggle.
		/// </summary>
		/// <value>The name of the toggle.</value>
		[ComVisible(true)]
		public abstract string ToggleName { get; }

		#endregion

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="Toggle" /> class.
		/// </summary>
		/// <param name="toggleCollection">The toggle collection.</param>
		public Toggle(ToggleCollection toggleCollection) {
			this.ToggleCollection = toggleCollection;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Enables this instance.
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
		/// Disables this instance.
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
		/// Initializes the specified configuration.
		/// </summary>
		/// <param name="togglesConfiguration">The configuration.</param>
		public void Initialize(XElement togglesConfiguration) {
			var toggleConfiguration = togglesConfiguration?.Element(this.ToggleName);
			if (toggleConfiguration != null) {
				this.Enabled = toggleConfiguration.SafeAttributeValue<bool>(nameof(Toggle.Enabled), false);
				this.ToggleCollection.JmcManager.JmcObject.ShowMe($@"Toggle {this.ToggleName} is {(this.Enabled ? @"Enabled" : @"Disabled")}");
				this.OnInitialize(toggleConfiguration);
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
		/// Processes the specified incoming line.
		/// </summary>
		/// <param name="incomingLine">The incoming line.</param>
		internal void Process(string incomingLine) {
			try {
				this.OnProcess(incomingLine);
			}
			catch { }
		}

		/// <summary>
		/// Saves the configuration.
		/// </summary>
		/// <returns>XElement.</returns>
		internal XElement SaveConfiguration() {
			var configuration = new XElement(this.ToggleName);
			try {
				this.ToggleCollection.JmcManager.JmcObject.ShowMe($@"Toggle {this.ToggleName} is {(this.Enabled ? @"Enabled" : @"Disabled")}");
				configuration.Add(new XAttribute(nameof(Toggle.Enabled), this.Enabled));
				this.OnConfigurationSaved(configuration);
			}
			catch { }
			return configuration;
		}

		#endregion

		#region Cascaded Methods

		protected virtual void OnConfigurationSaved(XElement configuration) { }

		/// <summary>
		/// Called when the implementing class must enable itself.
		/// </summary>
		protected virtual void OnEnable() { }

		/// <summary>
		/// Called when the implementing class must disable itself.
		/// </summary>
		protected virtual void OnDisable() { }

		/// <summary>
		/// Called when the implementing class must initialize itself.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		protected virtual void OnInitialize(XElement configuration) { }

		protected abstract void OnProcess(string incomingLine);

		#endregion

	}

}
