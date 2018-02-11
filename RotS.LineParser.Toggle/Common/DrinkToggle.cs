
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

	public class DrinkToggle
		: Toggle {

		#region Properties

		#region DrinkSource

		private const string DEFAULT_DRINK_SOURCE = @"waterskin";
		private string _drinkSource;

		/// <summary>
		/// Gets or sets the drink source.
		/// </summary>
		/// <value>The drink source.</value>
		public string DrinkSource {
			get { return _drinkSource; }
			set { _drinkSource = string.IsNullOrWhiteSpace(value) ? DEFAULT_DRINK_SOURCE : value; }
		}

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

		protected override void OnConfigurationSaved(XElement configuration) {
			base.OnConfigurationSaved(configuration);
			configuration.Add(
				new XAttribute(nameof(DrinkToggle.DrinkSource), this.DrinkSource)
				);
		}

		/// <summary>
		/// Called when the implementing class must initialize itself.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		protected override void OnInitialize(XElement configuration) {
			base.OnInitialize(configuration);
			this.DrinkSource = configuration.SafeAttributeValue<string>(nameof(DrinkToggle.DrinkSource), this.DrinkSource);
		}

		protected override void OnProcess(string incomingLine) {
			this.ToggleCollection.JmcManager.JmcObject.Send($@"drink {this.DrinkSource};drink {this.DrinkSource}");
		}

		#endregion

	}

}
