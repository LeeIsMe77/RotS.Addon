namespace RotS.Addon.Toggle.Common {

	#region Directives
	using System;
	using System.Runtime.InteropServices;

	#endregion

	/// <summary>
	/// Interface allowing specified methods and indexers to be COM-compatible and publicly visible on a <seealso cref="ToggleCollection"/>.
	/// </summary>
	[ComVisible(true)]
	[Guid(@"AB44F782-12A5-4571-8BC2-831B7A26C629")]
	public interface IToggleCollection {

		/// <summary>
		/// Gets the <see cref="Toggle"/> with the specified module name.
		/// </summary>
		/// <param name="moduleName">Name of the module.</param>
		/// <returns>Module.</returns>
		Toggle this[string moduleName] { get; }

		/// <summary>
		/// Searches for the <seealso cref="Toggle" /> by the provided name.  If the module exists, disable it.  Otherwise; quietly leave the method.
		/// </summary>
		/// <param name="moduleName">Name of the module.</param>
		void DisableToggle(string moduleName);

		/// <summary>
		/// Searches for the <seealso cref="Toggle" /> by the provided name.  If the module exists, enable it.  Otherwise; quietly leave the method.
		/// </summary>
		/// <param name="moduleName">Name of the module.</param>
		void EnableToggle(string moduleName);

	}

}
