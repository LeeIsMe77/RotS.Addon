namespace RotS.Addon.Toggle {

	#region Directives
	using RotS.Addon.Toggle.Common;
	#endregion

	/// <summary>
	/// Creates a <seealso cref="Toggle"/> for automatically rescuing a target.
	/// </summary>
	/// <seealso cref="Toggle" />
	public class RescueToggle
		: Toggle {

		#region Properties

		#region MatchPattern

		/// <summary>
		/// Gets the <seealso cref="Regex" /> match pattern used to determine if the <seealso cref="Toggle" /> should continue processing.
		/// </summary>
		/// <value>The match pattern.</value>
		public override string MatchPattern {
			get {
				return @"(.*) turns to fight (?!Fali!|Fimli!)([a-zA-Z]+)!"; ;
			}
		}

		#endregion

		#region ToggleName

		/// <summary>
		/// Gets the name of the toggle.
		/// </summary>
		/// <value>The name of the toggle.</value>
		public override string ToggleName {
			get {
				return nameof(RescueToggle);
			}
		}

		#endregion

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="RescueToggle"/> class.
		/// </summary>
		/// <param name="toggleCollection">The toggle collection.</param>
		public RescueToggle(ToggleCollection toggleCollection)
			: base(toggleCollection) { }

		#endregion

		#region Method Overrides

		protected override void OnProcess(string incomingLine) {
		}

		#endregion

	}

}
