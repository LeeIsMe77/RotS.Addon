namespace RotS.LineParser.Bot.Common {

	#region Directives

	#endregion

	/// <summary>
	/// Enumeration representing different timers utilized by the <seealso cref="PebbleslideJmcManager"/>.
	/// </summary>
	public enum PebbleslideTimer {

		/// <summary>
		/// The timer that fires when it is safe for a character to move.
		/// </summary>
		MoveTimer = 100,
		
		/// <summary>
		/// The timer that fires when a character must wait before being able to move.
		/// </summary>
		WaitTimer = 101

	}

}
