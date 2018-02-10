namespace RotS.LineParser.Pebbleslide {

	#region Directives
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Runtime.InteropServices;
	using System.Text.RegularExpressions;
	using System.Xml.Linq;
	using RotS.LineParser.Core.Common;
	using RotS.LineParser.Core.Extensions;
	using RotS.LineParser.Pebbleslide.Common;
	using TTCOREEXLib;
	#endregion

	/// <summary>
	/// A Pebbleslide <seealso cref="LineParser"/> designed for trigger spiriting in the spirit mines.
	/// </summary>
	/// <seealso cref="RotS.LineParser.Core.Common.LineParser" />
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[ComVisible(true)]
	[Guid("53C47A57-DB70-44FC-983D-1C9E0F24DB7A")]
	[ProgId("RotS.LineParser.PebbleslideParser")]
	public class PebbleslideParser
		: LineParser {

		#region Properties

		private string _target = null;
		private bool _moveTimerEnabled = false;
		private bool _waitTimerEnabled = false;

		#region BuffingSpells

		/// <summary>
		/// Gets or sets the buffing spells.
		/// </summary>
		/// <value>The buffing spells.</value>
		public List<string> BuffingSpells { get; set; } = new List<string> {
			@"infravision",
			@"slow digestion",
			@"revive",
		};

		#endregion

		#region CharacterName

		/// <summary>
		/// Gets or sets the name of the character.
		/// </summary>
		/// <value>The name of the character.</value>
		public string CharacterName { get; set; }

		#endregion

		#region MoveTime

		/// <summary>
		/// Gets or sets the move time.
		/// </summary>
		/// <value>The move time.</value>
		public int MoveTime { get; set; } = 50; /* 5 seconds */

		#endregion

		#region WaitTime

		/// <summary>
		/// Gets or sets the wait time.
		/// </summary>
		/// <value>The wait time.</value>
		public int WaitTime { get; set; } = 300; /* 30 Seconds */

		#endregion

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="PebbleslideParser" /> class.
		/// </summary>
		/// <param name="jmcObject">The JMC object.</param>
		/// <param name="configurationFile">The configuration file.</param>
		public PebbleslideParser()
			: base() { }

		#endregion

		#region Configuration

		/// <summary>
		/// Called when the configuration settings must be loaded.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		protected override void OnConfigurationSettingsLoaded(XElement configuration) {
			base.OnConfigurationSettingsLoaded(configuration);
			this.BuffingSpells = configuration
				.SafeElementValue(
					nameof(PebbleslideParser.BuffingSpells),
					string.Join(@";", this.BuffingSpells)
					)
				.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
				.ToList();
			this.CharacterName = configuration.SafeElementValue(nameof(PebbleslideParser.CharacterName), string.Empty);
			this.MoveTime = configuration.SafeElementValue(nameof(PebbleslideParser.MoveTime), this.MoveTime);
			this.WaitTime = configuration.SafeElementValue(nameof(PebbleslideParser.WaitTime), this.WaitTime);
		}

		/// <summary>
		/// Called when the configuration settings must be saved.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		protected override void OnConfigurationSettingsSaved(XElement configuration) {
			base.OnConfigurationSettingsSaved(configuration);
			configuration.Add(
				new XElement(nameof(PebbleslideParser.BuffingSpells), string.Join(@";", this.BuffingSpells)),
				new XElement(nameof(PebbleslideParser.CharacterName), this.CharacterName),
				new XElement(nameof(PebbleslideParser.MoveTime), this.MoveTime),
				new XElement(nameof(PebbleslideParser.WaitTime), this.WaitTime)
				);
		}

		#endregion

		#region Base Overrides

		/// <summary>
		/// Called when an incoming line has been received from the server.
		/// </summary>
		/// <param name="incomingLine">The incoming line.</param>
		protected override void OnIncoming(string incomingLine) {
			base.OnIncoming(incomingLine);

			// If these triggers are not enabled, do not continue processing.
			if (!this.Enabled) {
				return;
			}

			if (string.IsNullOrWhiteSpace(incomingLine)) {
				return;
			}

			switch (incomingLine.Trim()) {

				#region Navigation
				case @"Ghostly chills and wails can be heard constantly here, filling the tunnels":
					this.JmcObject.Navigate(Direction.West);
					return;
				case @"Long ago, this place used to lie at the entrance to a very prosperous metal":
					this.JmcObject.Send(@"open cavein");
					this.JmcObject.Navigate(Direction.West);
					return;
				case @"Many sticky cobwebs crisscross the walls here, forming a large wall of":
					this.JmcObject.Navigate(Direction.West);
					return;
				case @"Piles upon piles of stones lie to the east, filling in the large archway":
					this.JmcObject.Navigate(Direction.West);
					return;
				case @"Strange occurrences constantly happen in this place.  The temperature of":
					this.JmcObject.Navigate(Direction.East);
					return;
				case @"The continuation of these mines to the west is blocked off by a large":
					this.JmcObject.Navigate(Direction.East);
					return;
				case @"The mines have sloped downward here from the north, making breathing just a":
					this.JmcObject.Navigate(Direction.South);
					return;
				case @"The mines take a bend to the north here.  Large iron carts line the walls.":
					this.JmcObject.Navigate(Direction.North);
					return;
				case @"The tunnel bends extremely sharply to the west and south here.  The walls":
					this.JmcObject.Navigate(Direction.West);
					return;
				case @"The tunnel comes to a halt here.  There are several large iron carts":
					this.JmcObject.Navigate(Direction.South, Direction.South);
					return;
				case @"The tunnel forks into two different directions here, north and south.":
					this.JmcObject.Navigate(Direction.North);
					return;
				case @"The tunnel of the minds comes to a fork here, to the north, east, and":
					this.JmcObject.Navigate(Direction.North);
					return;
				case @"The tunnel of the mines takes a steep downward slope to the south here.":
					this.JmcObject.Navigate(Direction.South);
					return;
				case @"The tunnel of this mine bends to the north and west here.  Many large iron":
					this.JmcObject.Navigate(Direction.West);
					return;
				case @"The tunnels of this mine are constantly filled with echoes and cries for":
					this.JmcObject.Navigate(Direction.North);
					return;
				case @"This end of the mines has come to be due to the massive barricade of":
					_moveTimerEnabled = false;
					this.JmcObject.KillTimer((int)PebbleslideTimer.MoveTimer);
					_waitTimerEnabled = true;
					this.JmcObject.SetTimer((int)PebbleslideTimer.WaitTimer, this.WaitTime);
					this.JmcObject.Navigate(Direction.North, Direction.East, Direction.East, Direction.East, Direction.South, Direction.South, Direction.West, Direction.West);
					this.JmcObject.Send(@"open brokenhatch");
					this.JmcObject.Navigate(Direction.Up, Direction.North, Direction.North, Direction.East, Direction.North, Direction.North, Direction.East, Direction.East);
					// TODO: buffMe, reviveMe, whoAmI
					return;
				case @"This mine seems to be utterly old.  The wooden support beams that line the":
					this.JmcObject.Navigate(Direction.West);
					return;
				case @"This part of the mines comes to a complete halt, due to the large mass of":
					this.JmcObject.Navigate(Direction.South, Direction.South);
					return;
				case @"This part of the mines comes to a dead end.  Large piles of earth lie":
					this.JmcObject.Send(@"open brokenhatch");
					this.JmcObject.Navigate(Direction.Down);
					return;
				case @"This place is filled with the sounds of screaming and crying.  Large heaps":
					this.JmcObject.Navigate(Direction.South);
					return;
				#endregion

				#region Targeting

				case @"A flickering ball of pale flame dances through the air. (shadow)":
					_target = @"flame";
					this.Log($@"Target is now {_target}.", @"green");
					return;
				case @"A grey spirit gazes upon you. (shadow)":
					_target = @"spirit";
					this.Log($@"Target is now {_target}.", @"green");
					return;
				case @"A hazy figure glides above the ground. (shadow)":
					_target = @"wraith";
					this.Log($@"Target is now {_target}.", @"green");
					return;
				case @"A phantom, a spirit of discontent, moans as it floats here. (shadow)":
					_target = @"phantom";
					this.Log($@"Target is now {_target}.", @"green");
					return;
				case @"The ghost of a miner is hovering here over a pile of rocks. (shadow)":
					_target = @"ghost";
					this.Log($@"Target is now {_target}.", @"green");
					return;

				#endregion

				#region Character Actions

				case @"You hit yourself...OUCH!.":
					this.JmcObject.Send(@"set mental on");
					this.JmcObject.Send(@"examine");
					return;
				case @"You lost your concentration!":
					this.JmcObject.Send(@"!"); // Repeat the previous command.
					return;
				case @"You contemplate yourself for a little while.":
				case @"Whom do you want to press?":
					_target = this.CharacterName;
					this.JmcObject.Send(@"examine");
					return;

				#endregion
			}

			if (Regex.IsMatch(incomingLine, @"^You force your Will against .*'s .*!$")) {
				if (_moveTimerEnabled) {
					_moveTimerEnabled = false;
					this.JmcObject.KillTimer((int)PebbleslideTimer.MoveTimer);
				}
			}

			if (Regex.IsMatch(incomingLine, @"^Your spirit increases by \d+\.$")) {
				_target = this.CharacterName;
				this.JmcObject.Send(@"examine");
				_moveTimerEnabled = true;
				this.JmcObject.SetTimer((int)PebbleslideTimer.MoveTimer, this.MoveTime);
				return;
			}

		}

		/// <summary>
		/// Called when a timer has fired.
		/// </summary>
		/// <param name="timerID">The timer identifier.</param>
		protected override void OnTimer(int timerID) {
			base.OnTimer(timerID);
			// If the timerID is not a recognized PebbleslideTimer, don't continue processing.
			if (!Enum.IsDefined(typeof(PebbleslideTimer), timerID)) {
				return;
			}
			// ...otherwise, figure out which timer was called.
			var pebbleslideTimer = (PebbleslideTimer)timerID;
			switch (pebbleslideTimer) {
				// If the MoveTimer was called...
				case PebbleslideTimer.MoveTimer:
					this.Log($@"Target is registered as {_target}.", @"red");
					// ...it's time to move, examine the room.
					this.JmcObject.Send($@"kill {_target ?? this.CharacterName}");
					break;
				// If the WaitTimer was called...
				case PebbleslideTimer.WaitTimer:
					// ...kill the wait timer, and start moving.
					this.JmcObject.KillTimer((int)PebbleslideTimer.WaitTimer);
					this.JmcObject.SetTimer((int)PebbleslideTimer.MoveTimer, this.MoveTime);
					break;
			}

		}

		#endregion

		#region Methods

		/// <summary>
		/// Disables the parsing of lines and removes all timers.
		/// </summary>
		protected override void OnDisable() {
			base.OnDisable();
			try {
				// ...Kill all timers.
				_moveTimerEnabled = false;
				this.JmcObject.KillTimer((int)PebbleslideTimer.MoveTimer);
				_waitTimerEnabled = false;
				this.JmcObject.KillTimer((int)PebbleslideTimer.WaitTimer);
			}
			catch { }
		}

		/// <summary>
		/// Enables the parsing of lines and enables the <seealso cref="PebbleslideTimer.MoveTimer"/>.
		/// </summary>
		protected override void OnEnable() {
			try {
				// Set the MoveTimer to the specified duration.
				_waitTimerEnabled = false;
				_moveTimerEnabled = true;
				this.JmcObject.KillTimer((int)PebbleslideTimer.WaitTimer);
				this.JmcObject.SetTimer((int)PebbleslideTimer.MoveTimer, this.MoveTime);
			}
			catch { }
		}

		#endregion

	}

}
