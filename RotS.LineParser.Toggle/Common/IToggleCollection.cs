namespace RotS.LineParser.Toggle.Common {

	#region Directives
	using System;
	using System.Runtime.InteropServices;

	#endregion

	[ComVisible(true)]
	[Guid(@"AB44F782-12A5-4571-8BC2-831B7A26C629")]
	public interface IToggleCollection {

		Toggle this[string toggleName] { get; }

		void DisableToggle(string toggleName);

		void EnableToggle(string toggleName);

	}

}
