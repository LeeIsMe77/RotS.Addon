namespace RotS.LineParser.Bot.Common {

	#region Directives
	using System;
	using System.Runtime.InteropServices;
	#endregion

	[ComVisible(true)]
	[Guid(@"C1211F32-D4D8-4BB7-A29C-9DC52CA2658D")]
	public interface IEnhancementSkillCollection {

		/// <summary>
		/// Gets the count.
		/// </summary>
		/// <value>The count.</value>
		int Count { get; }

		/// <summary>
		/// Gets the <see cref="EnhancementSkill"/> with the specified enhancement skill name.
		/// </summary>
		/// <param name="enhancementSkillName">Name of the enhancement skill.</param>
		/// <returns>EnhancementSkill.</returns>
		EnhancementSkill this[string enhancementSkillName] { get; }

		/// <summary>
		/// Adds the specified enhancement skill name.
		/// </summary>
		/// <param name="enhancementSkillName">Name of the enhancement skill.</param>
		/// <param name="targetSelf">if set to <c>true</c> [target self].</param>
		/// <returns>EnhancementSkill.</returns>
		EnhancementSkill Add(string enhancementSkillName, bool targetSelf);

		/// <summary>
		/// Adds the specified enhancement skill.
		/// </summary>
		/// <param name="enhancementSkill">The enhancement skill.</param>
		void Add(EnhancementSkill enhancementSkill);

		/// <summary>
		/// Removes the specified enhancement skill name.
		/// </summary>
		/// <param name="enhancementSkillName">Name of the enhancement skill.</param>
		void Remove(string enhancementSkillName);

		/// <summary>
		/// Removes the specified enhancement skill.
		/// </summary>
		/// <param name="enhancementSkill">The enhancement skill.</param>
		void Remove(EnhancementSkill enhancementSkill);

	}

}
