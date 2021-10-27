using System;
using System.Collections.Generic;
using Terraria;


namespace PotLuck {
	public delegate bool PotBreakFunc( int tileX, int tileY, out IList<int> droppedItemIndexes );

	
	public delegate void PostPotBreakFunc( int tileX, int tileY, IList<int> droppedItemIndexes );




	public partial class PotLuckAPI {
		public static void AddPotBreakAction( PotBreakFunc action ) {
			PotLuckMod.Instance.OnPotBreakActions.Add( action );
		}

		public static void AddPostPotBreakAction( PostPotBreakFunc action ) {
			PotLuckMod.Instance.OnPotBreakItemDropActions.Add( action );
		}
	}
}