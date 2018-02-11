namespace RotS.LineParser.Bot.Common {

	#region Directives
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Runtime.InteropServices;
	#endregion

	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	[Guid(@"75751862-2D7A-4C33-AF92-EAE270F19CDC")]
	public class EnhancementSkillCollection
		: List<EnhancementSkill>, IEnhancementSkillCollection {

		#region Indexer

		/// <summary>
		/// Gets the <see cref="EnhancementSkill"/> with the specified enhancement skill name.
		/// </summary>
		/// <param name="enhancementSkillName">Name of the enhancement skill.</param>
		/// <returns>EnhancementSkill.</returns>
		[ComVisible(true)]
		public EnhancementSkill this[string enhancementSkillName] {
			get {
				return this.FirstOrDefault(enhancementSkill => enhancementSkill.EnhancementSkillName.Equals(enhancementSkillName)); ;
			}
		}

		#endregion

		#region Collection Management

		/// <summary>
		/// Adds the specified enhancement skill name.
		/// </summary>
		/// <param name="enhancementSkillName">Name of the enhancement skill.</param>
		/// <param name="targetSelf">if set to <c>true</c> [target self].</param>
		/// <returns>EnhancementSkill.</returns>
		[ComVisible(true)]
		public EnhancementSkill Add(string enhancementSkillName, bool targetSelf = false) {
			var enhancementSkill = new EnhancementSkill(enhancementSkillName, targetSelf);
			this.Add(enhancementSkill);
			return enhancementSkill;
		}

		/// <summary>
		/// Removes the specified enhancement skill name.
		/// </summary>
		/// <param name="enhancementSkillName">Name of the enhancement skill.</param>
		[ComVisible(true)]
		public void Remove(string enhancementSkillName) {
			var enhancementSkill = this[enhancementSkillName];
			if (enhancementSkill != null) {
				this.Remove(enhancementSkill);
			}
		}

		/// <summary>
		/// Removes the specified enhancement skill.
		/// </summary>
		/// <param name="enhancementSkill">The enhancement skill.</param>
		[ComVisible(true)]
		public new void Remove(EnhancementSkill enhancementSkill) {
			base.Remove(enhancementSkill);
		}

		#endregion

	}

}
