using HamstarHelpers.Helpers.TModLoader.Mods;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;


namespace PotLuck {
	public partial class PotLuckMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-potluck-mod";


		////////////////

		public static PotLuckMod Instance;



		////////////////

		internal (int x, int y) PotBreakTile = (0, 0);

		internal IList<Func<(int, int), Item[]>> OnPotBreakActions = new List<Func<(int, int), Item[]>>();

		internal IList<Action<(int, int), Item[]>> OnPotBreakItemDropActions = new List<Action<(int, int), Item[]>>();



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
			return ModBoilerplateHelpers.HandleModCall( typeof(PotLuckAPI), args );
		}
	}
}