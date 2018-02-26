namespace RotS.Addon.Core.Extensions {

	#region Directives
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using RotS.Addon.Core.Common;
	#endregion

	public static class EnumExtension {

		/// <summary>
		/// Gets the JMC color value of the <seealso cref="ColorValueAttribute"/> attribute on the specified Enum value.  If the enum 
		/// does not contain the attribute, an empty string is returned.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>System.String.</returns>
		public static string GetJmcColorValue(this Enum value) {
			return value
				.GetType()
				.GetCustomAttributes(typeof(ColorValueAttribute), false)
				.OfType<ColorValueAttribute>()
				.FirstOrDefault()
				?.Value
				?? string.Empty
				;
		}

	}

}
