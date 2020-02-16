using System;
using Terraria;


namespace PotLuck {
	public partial class PotLuckAPI {
		public static void AddPotBreakAction( Func<(int, int), Item[]> action ) {
			PotLuckMod.Instance.OnPotBreakActions.Add( action );
		}

		public static void AddPostPotBreakAction( Action<(int, int), Item[]> action ) {
			PotLuckMod.Instance.OnPotBreakItemDropActions.Add( action );
		}
	}
}