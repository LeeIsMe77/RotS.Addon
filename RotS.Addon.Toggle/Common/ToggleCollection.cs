namespace RotS.Addon.Toggle.Common {

	#region Directives
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Runtime.InteropServices;
	#endregion

	/// <summary>
	/// A class containing a collection of <seealso cref="Toggle"/> objects.
	/// </summary>
	/// <seealso cref="System.Collections.Generic.List{RotS.Addon.Toggle.Common.Toggle}" />
	/// <seealso cref="RotS.Addon.Toggle.Common.IToggleCollection" />
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	[Guid(@"9C6C8C0F-A5B2-4F5B-B337-C98C52478AA0")]
	public class ToggleCollection
		: List<Toggle>, IToggleCollection {

		#region Properties

		/// <summary>
		/// Gets the <seealso cref="Toggles.ToggleModule"/> that owns this collection.
		/// </summary>
		/// <value>The JMC manager.</value>
		[ComVisible(true)]
		public ToggleModule ToggleModule { get; }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ToggleCollection" /> class.
		/// </summary>
		/// <param name="toggleModule">The toggle module.</param>
		/// <exception cref="ArgumentNullException">toggleModule - The JMC Manager cannot be null.</exception>
		internal ToggleCollection(ToggleModule toggleModule) {
			this.ToggleModule = toggleModule ?? throw new ArgumentNullException(nameof(toggleModule), @"The JMC Manager cannot be null.");
		}

		#endregion

		#region Indexer

		/// <summary>
		/// Gets the <see cref="Toggle"/> with the provided toggle name.
		/// </summary>
		/// <param name="toggleName">Name of the toggle.</param>
		/// <returns>Toggle.</returns>
		[ComVisible(true)]
		public Toggle this[string toggleName] {
			get {
				var firstOrDefault = this.FirstOrDefault(toggle => toggle.ToggleName.Equals(toggleName, StringComparison.OrdinalIgnoreCase));
				if (firstOrDefault == null) {
					this.ToggleModule.JmcObject.ShowMe(@"it's null!", @"red");
				}
				else {
					this.ToggleModule.JmcObject.ShowMe(firstOrDefault.ToggleName, @"green");
				}
				return firstOrDefault;
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Searches for the <seealso cref="Toggle" /> by the provided name.  If the toggle exists, disable it.  Otherwise; quietly leave the method.
		/// </summary>
		/// <param name="toggleName">Name of the toggle.</param>
		[ComVisible(true)]
		public void DisableToggle(string toggleName) {
			this[toggleName]?.Enable();
		}

		/// <summary>
		/// Searches for the <seealso cref="Toggle" /> by the provided name.  If the toggle exists, enable it.  Otherwise; quietly leave the method.
		/// </summary>
		/// <param name="toggleName">Name of the toggle.</param>
		[ComVisible(true)]
		public void EnableToggle(string toggleName) {
			this[toggleName]?.Disable();
		}

		#endregion

	}

}
