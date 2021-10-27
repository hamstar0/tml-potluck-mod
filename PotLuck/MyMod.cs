using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.TModLoader.Mods;


namespace PotLuck {
	public partial class PotLuckMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-potluck-mod";


		////////////////

		public static PotLuckMod Instance;



		////////////////

		internal (int x, int y) PotBreakTile = (0, 0);

		internal IList<PotBreakFunc> OnPotBreakActions = new List<PotBreakFunc>();

		internal IList<PostPotBreakFunc> OnPotBreakItemDropActions = new List<PostPotBreakFunc>();



		////////////////

		public PotLuckMod() {
			PotLuckMod.Instance = this;
		}

		////

		public override void Unload() {
			PotLuckMod.Instance = null;
		}


		////////////////

		public override object Call( params object[] args ) {
			return ModBoilerplateLibraries.HandleModCall( typeof(PotLuckAPI), args );
		}
	}
}