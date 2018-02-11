namespace RotS.LineParser.Toggle.Common {

	#region Directives
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Xml.Linq;
	using RotS.LineParser.Core.Extensions;

	#endregion

	public class FoodToggle
		: Toggle {

		#region Properties

		#region FoodSource

		private const string DEFAULT_FOOD_SOURCE = @"waybread";
		private string _foodSource;

		/// <summary>
		/// Gets or sets the food source.
		/// </summary>
		/// <value>The food source.</value>
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

		protected override void OnConfigurationSaved(XElement configuration) {
			base.OnConfigurationSaved(configuration);
			configuration.Add(
				new XAttribute(nameof(FoodToggle.FoodSource), this.FoodSource)
				);
		}

		/// <summary>
		/// Called when the implementing class must initialize itself.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		protected override void OnInitialize(XElement configuration) {
			base.OnInitialize(configuration);
			this.FoodSource = configuration.SafeAttributeValue<string>(nameof(FoodToggle.FoodSource), this.FoodSource);
		}
		
		protected override void OnProcess(string incomingLine) {
			this.ToggleCollection.JmcManager.JmcObject.Send($@"get {this.FoodSource} all;eat {this.FoodSource}");
		}

		#endregion

	}

}
