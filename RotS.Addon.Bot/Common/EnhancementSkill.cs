namespace RotS.Addon.Bot.Common {

	#region Directives
	using System;
	using System.Runtime.InteropServices;
	#endregion

	/// <summary>
	/// Represents a single enhancement skill a character is capable of casting throughout the trigger process.
	/// </summary>
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	[Guid(@"CCE9ED37-43EF-4921-8275-12E932D75513")]
	public class EnhancementSkill {

		#region Properties

		/// <summary>
		/// Gets or sets the name of the enhancement skill.
		/// </summary>
		/// <value>The name of the enhancement skill.</value>
		[ComVisible(true)]
		public string EnhancementSkillName { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [target self].
		/// </summary>
		/// <value><c>true</c> if [target self]; otherwise, <c>false</c>.</value>
		[ComVisible(true)]
		public bool TargetSelf { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="EnhancementSkill"/> class.
		/// </summary>
		/// <param name="enhancementSkillName">Name of the enhancement skill.</param>
		/// <param name="targetSelf">if set to <c>true</c> [target self].</param>		
		public EnhancementSkill(string enhancementSkillName, bool targetSelf = false) {
			this.EnhancementSkillName = enhancementSkillName;
			this.TargetSelf = targetSelf;
		}

		#endregion

	}

}
