namespace RotS.Addon.Core.Common {

	#region Directives
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	#endregion

	/// <summary>
	/// Enumeration representing the text value recognized by JMC for a <seealso cref="JmcColors"/> color.
	/// </summary>
	/// <seealso cref="System.Attribute" />
	public class ColorValueAttribute
		: Attribute {

		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <value>The value.</value>
		public string Value { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ColorValueAttribute"/> class.
		/// </summary>
		/// <param name="value">The value.</param>
		public ColorValueAttribute(string value) {
			this.Value = value;
		}

	}

}
