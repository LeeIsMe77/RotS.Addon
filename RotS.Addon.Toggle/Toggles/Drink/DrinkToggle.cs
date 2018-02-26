namespace RotS.Addon.Toggle { 

	#region Directives
	using System.Runtime.InteropServices;
	using System.Xml.Linq;
	using RotS.Addon.Core.Extensions;
	using RotS.Addon.Toggle.Common;
	#endregion

	/// <summary>
	/// Creates a <seealso cref="Toggle"/> for automatically drinking from a watersource when you are thirsty.
	/// </summary>
	/// <seealso cref="Toggle" />
	public class DrinkToggle
		: Toggle {

		#region Properties

		#region DrinkContainer

		private const string DEFAULT_DRINK_CONTAINER = @"all";
		private string _drinkContainer = DEFAULT_DRINK_CONTAINER;

		/// <summary>
		/// Gets or sets the drink container.
		/// </summary>
		/// <value>The drink container.</value>
		[ComVisible(true)]
		public string DrinkContainer {
			get { return _drinkContainer; }
			set { _drinkContainer = string.IsNullOrWhiteSpace(value) ? DEFAULT_DRINK_CONTAINER : value; }
		}

		#endregion

		#region DrinkSource

		private const string DEFAULT_DRINK_SOURCE = @"waterskin";
		private string _drinkSource;

		/// <summary>
		/// Gets or sets the drink source.
		/// </summary>
		/// <value>The drink source.</value>
		[ComVisible(true)]
		public string DrinkSource {
			get { return _drinkSource; }
			set { _drinkSource = string.IsNullOrWhiteSpace(value) ? DEFAULT_DRINK_SOURCE : value; }
		}

		#endregion

		#region InContainer

		/// <summary>
		/// Gets a value indicating whether the drink is stored within a container.
		/// </summary>
		/// <value><c>true</c> if the drink is in a container; otherwise, <c>false</c>.</value>
		[ComVisible(true)]
		public bool InContainer { get; set; } = false;

		#endregion

		#region MatchPattern

		/// <summary>
		/// Gets the match pattern.
		/// </summary>
		/// <value>The match pattern.</value>
		public override string MatchPattern {
			get { return @"^You are thirsty.$"; }
		}

		#endregion

		#region Repetitions

		private const int DEFAULT_REPETITIONS = 1;
		private int _repetitions = DEFAULT_REPETITIONS;

		/// <summary>
		/// Gets or sets the repetitions.
		/// </summary>
		/// <value>The repetitions.</value>
		[ComVisible(true)]
		public int Repetitions {
			get { return _repetitions; }
			set { _repetitions = value <= 0 ? DEFAULT_REPETITIONS : value; }
		}

		#endregion

		#region ReturnToContainer

		/// <summary>
		/// Gets or sets a value indicating whether the drink source should be returned to the container it was retrieved from (if applicable).
		/// </summary>
		/// <value><c>true</c> if returning the drink source to the container; otherwise, <c>false</c>.</value>
		[ComVisible(true)]
		public bool ReturnToContainer { get; set; } = false;

		#endregion

		#region ToggleName

		/// <summary>
		/// Gets the name of the toggle.
		/// </summary>
		/// <value>The name of the toggle.</value>
		public override string ToggleName {
			get { return nameof(DrinkToggle); }
		}

		#endregion

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DrinkToggle" /> class.
		/// </summary>
		/// <param name="toggleCollection">The toggle collection.</param>
		public DrinkToggle(ToggleCollection toggleCollection)
			: base(toggleCollection) { }

		#endregion

		#region Method Overrides

		/// <summary>
		/// Called when the initial configuration settings have been saved and the implementing class must save custom property settings.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		protected override void OnConfigurationSaved(XElement configuration) {
			base.OnConfigurationSaved(configuration);
			configuration.Add(
				new XAttribute(nameof(DrinkToggle.DrinkContainer), this.DrinkContainer),
				new XAttribute(nameof(DrinkToggle.DrinkSource), this.DrinkSource),
				new XAttribute(nameof(DrinkToggle.InContainer), this.InContainer),
				new XAttribute(nameof(DrinkToggle.Repetitions), this.Repetitions),
				new XAttribute(nameof(DrinkToggle.ReturnToContainer), this.ReturnToContainer)
				);
		}

		/// <summary>
		/// Called when the implementing class must initialize custom property settings from the configuration.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		protected override void OnInitialize(XElement configuration) {
			base.OnInitialize(configuration);
			this.DrinkContainer = configuration.SafeAttributeValue<string>(nameof(DrinkToggle.DrinkContainer), this.DrinkContainer);
			this.DrinkSource = configuration.SafeAttributeValue<string>(nameof(DrinkToggle.DrinkSource), this.DrinkSource);
			this.InContainer = configuration.SafeAttributeValue<bool>(nameof(DrinkToggle.InContainer), this.InContainer);
			this.Repetitions = configuration.SafeAttributeValue<int>(nameof(DrinkToggle.Repetitions), this.Repetitions);
			this.ReturnToContainer = configuration.SafeAttributeValue<bool>(nameof(DrinkToggle.ReturnToContainer), this.ReturnToContainer);
		}

		/// <summary>
		/// Called when the implementing class must provide custom processing code after a match has been determined.
		/// </summary>
		/// <param name="incomingLine">The incoming line.</param>
		protected override void OnProcess(string incomingLine) {

			if (this.InContainer) {
				this.ToggleCollection.ToggleModule.JmcObject.Send($@"get {this.DrinkSource} {this.DrinkContainer}");
			}

			for (var repetitions = this.Repetitions; repetitions > 0; repetitions--) {
				this.ToggleCollection.ToggleModule.JmcObject.Send($@"drink {this.DrinkSource}");
			}

			if (this.ReturnToContainer) {
				this.ToggleCollection.ToggleModule.JmcObject.Send($@"put {this.DrinkSource} {this.DrinkContainer}");
			}

		}

		#endregion

	}

}