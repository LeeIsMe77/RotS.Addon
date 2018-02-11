namespace RotS.LineParser.Toggle.Common {

	#region Directives
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;
	using System.Runtime.InteropServices;

	#endregion

	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	[Guid(@"9C6C8C0F-A5B2-4F5B-B337-C98C52478AA0")]
	public class ToggleCollection
		: Collection<Toggle>, IToggleCollection {

		#region Properties

		/// <summary>
		/// Gets the JMC manager.
		/// </summary>
		/// <value>The JMC manager.</value>
		[ComVisible(true)]
		public ToggleJmcManager JmcManager { get; }

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ToggleCollection"/> class.
		/// </summary>
		/// <param name="jmcManager">The JMC manager.</param>
		/// <exception cref="System.ArgumentNullException">jmcManager - The JMC Manager cannot be null.</exception>
		internal ToggleCollection(ToggleJmcManager jmcManager) {
			this.JmcManager = jmcManager ?? throw new ArgumentNullException(nameof(jmcManager), @"The JMC Manager cannot be null.");
		}

		#endregion

		#region Index

		/// <summary>
		/// Gets the <see cref="Toggle"/> with the specified toggle name.
		/// </summary>
		/// <param name="toggleName">Name of the toggle.</param>
		/// <returns>Toggle.</returns>
		[ComVisible(true)]
		public Toggle this[string toggleName] {
			get {
				var firstOrDefault = this.FirstOrDefault(toggle => toggle.ToggleName.Equals(toggleName, StringComparison.OrdinalIgnoreCase));
				if (firstOrDefault == null) {
					this.JmcManager.JmcObject.ShowMe(@"it's null!", @"red");
				}
				else {
					this.JmcManager.JmcObject.ShowMe(firstOrDefault.ToggleName, @"green");
				}
				return firstOrDefault;
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Adds the range of <seealso cref="Toggle"/> objects to this collection.
		/// </summary>
		/// <param name="toggles">The toggles.</param>
		internal void AddRange(IEnumerable<Toggle> toggles) {
			foreach (var toggle in toggles) {
				this.Add(toggle);
			}
		}

		/// <summary>
		/// Disables the toggle by the provided name.
		/// </summary>
		/// <param name="toggleName">Name of the toggle.</param>
		[ComVisible(true)]
		public void DisableToggle(string toggleName) {
			var toggle = this[toggleName];
			if (toggle != null) {
				this[this.IndexOf(toggle)].Enable();
			}
		}

		/// <summary>
		/// Enables the toggle by the provided name.
		/// </summary>
		/// <param name="toggleName">Name of the toggle.</param>
		[ComVisible(true)]
		public void EnableToggle(string toggleName) {
			var toggle = this[toggleName];
			if (toggle != null) {
				this[this.IndexOf(toggle)].Disable();
			}
		}

		#endregion

	}

}
