namespace RotS.Addon.Toggle {

	#region Directives
	using System.Runtime.InteropServices;
	using System.Xml.Linq;
	using RotS.Addon.Core.Extensions;
	using RotS.Addon.Toggle.Common;
	#endregion

	/// <summary>
	/// Creates a <seealso cref="Toggle"/> for automatically eat a food source when you are hungry.
	/// </summary>
	/// <seealso cref="Toggle" />
	public class FoodToggle
		: Toggle {

		#region Properties

		#region InContainer

		/// <summary>
		/// Gets a value indicating whether the food is stored within a container.
		/// </summary>
		/// <value><c>true</c> if the food is in a container; otherwise, <c>false</c>.</value>
		[ComVisible(true)]
		public bool InContainer { get; set; } = true;

		#endregion

		#region FoodContainer

		private const string DEFAULT_FOOD_CONTAINER = @"all";
		private string _foodContainer = DEFAULT_FOOD_CONTAINER;

		/// <summary>
		/// Gets or sets the food container.
		/// </summary>
		/// <value>The food container.</value>
		[ComVisible(true)]
		public string FoodContainer {
			get { return _foodContainer; }
			set { _foodContainer = string.IsNullOrWhiteSpace(value) ? DEFAULT_FOOD_CONTAINER : value; }
		}

		#endregion

		#region FoodSource

		private const string DEFAULT_FOOD_SOURCE = @"waybread";
		private string _foodSource = DEFAULT_FOOD_SOURCE;

		/// <summary>
		/// Gets or sets the food source.
		/// </summary>
		/// <value>The food source.</value>
		[ComVisible(true)]
		public string FoodSource {
			get { return _foodSource; }
			set { _foodSource = string.IsNullOrWhiteSpace(value) ? DEFAULT_FOOD_SOURCE : value; }
		}

		#endregion

		#region MatchPattern

		/// <summary>
		/// Gets the match pattern.
		/// </summary>
		/// <value>The match pattern.</value>
		public override string MatchPattern {
			get { return @"^You are hungry.$"; }
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

		#region ToggleName

		/// <summary>
		/// Gets the name of the toggle.
		/// </summary>
		/// <value>The name of the toggle.</value>
		public override string ToggleName {
			get { return nameof(FoodToggle); }
		}

		#endregion

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="FoodToggle" /> class.
		/// </summary>
		/// <param name="toggleCollection">The toggle collection.</param>
		public FoodToggle(ToggleCollection toggleCollection)
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
				new XAttribute(nameof(FoodToggle.FoodContainer), this.FoodContainer),
				new XAttribute(nameof(FoodToggle.FoodSource), this.FoodSource),
				new XAttribute(nameof(FoodToggle.InContainer), this.InContainer),
				new XAttribute(nameof(FoodToggle.Repetitions), this.Repetitions)
				);
		}

		/// <summary>
		/// Called when the implementing class must initialize custom property settings from the configuration.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		protected override void OnInitialize(XElement configuration) {
			base.OnInitialize(configuration);
			this.FoodContainer = configuration.SafeAttributeValue<string>(nameof(FoodToggle.FoodContainer), this.FoodContainer);
			this.FoodSource = configuration.SafeAttributeValue<string>(nameof(FoodToggle.FoodSource), this.FoodSource);
			this.InContainer = configuration.SafeAttributeValue<bool>(nameof(FoodToggle.InContainer), this.InContainer);
			this.Repetitions = configuration.SafeAttributeValue<int>(nameof(FoodToggle.Repetitions), this.Repetitions);
		}

		/// <summary>
		/// Called when the implementing class must provide custom processing code after a match has been determined.
		/// </summary>
		/// <param name="incomingLine">The incoming line.</param>
		protected override void OnProcess(string incomingLine) {
			// And for each repetition, execute the line.
			for (var repetitions = this.Repetitions; repetitions > 0; repetitions--) {
				if (this.InContainer) {
					this.ToggleCollection.ToggleModule.JmcObject.Send($@"get {this.FoodSource} {this.FoodContainer}");
				}
				this.ToggleCollection.ToggleModule.JmcObject.Send($@"eat {this.FoodSource}");
			}
		}

		#endregion

	}

}
