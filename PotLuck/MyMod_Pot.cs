using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Utilities;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.TModLoader;
using ModLibsGeneral.Libraries.World;


namespace PotLuck {
	public partial class PotLuckMod : Mod {
		internal static void ProcessPotBreak( int tileX, int tileY ) {
			IList<int> itemIdxs;

			var potEntries = PotLuckConfig.Instance.Get<List<PotEntry>>(
				nameof(PotLuckConfig.PotEntries)
			);

			//

			foreach( PotBreakFunc func in PotLuckMod.Instance.OnPotBreakActions ) {
				if( func.Invoke(tileX, tileY, out itemIdxs) ) {
					PotLuckMod.PostProcessPotItems( tileX, tileY, itemIdxs );
				}
			}

			//

			foreach( PotEntry potEnt in potEntries ) {
				if( PotLuckMod.CheckPotEntryIf(potEnt, tileX, tileY, out itemIdxs) ) {
					PotLuckMod.PostProcessPotItems( tileX, tileY, itemIdxs.ToArray() );
				}
			}
		}

		internal static void PostProcessPotItems( int tileX, int tileY, IList<int> droppedItemIndexes ) {
			foreach( PostPotBreakFunc action in PotLuckMod.Instance.OnPotBreakItemDropActions ) {
				action.Invoke( tileX, tileY, droppedItemIndexes );
			}
		}


		////

		private static bool CheckPotEntryIf( PotEntry potEnt, int tileX, int tileY, out IList<int> droppedItemIndexes ) {
			droppedItemIndexes = new List<int>();

			if( potEnt.HardModeOnly && !Main.hardMode ) {
				return false;
			}

			int surfBot = WorldLocationLibraries.SurfaceLayerBottomTileY;
			int undTop = WorldLocationLibraries.UnderworldLayerTopTileY;

			if( !potEnt.IsSurface && tileY < surfBot ) {
				return false;
			}
			if( !potEnt.IsCaves && tileY >= surfBot && tileY < undTop ) {
				return false;
			}
			if( !potEnt.IsUnderworld && tileY >= undTop ) {
				return false;
			}

			UnifiedRandom rand = TmlLibraries.SafelyGetRand();
			if( rand.NextFloat() > potEnt.PercentChance ) {
				return false;
			}

			//

			IEnumerable<PotItemEntry> itemDefs = potEnt.ItemDefs;
			if( potEnt.RandomItemPicks >= 1 ) {
				itemDefs = itemDefs
					.OrderBy( i => Main.rand.Next() )
					.Take( potEnt.RandomItemPicks );
			}

			foreach( PotItemEntry itemEnt in itemDefs ) {
				var pos = new Vector2( tileX<<4, tileY<<4 );
				int stack = rand.Next( itemEnt.MinStack, itemEnt.MaxStack );

				if( stack <= 0 ) {
					continue;
				}

				int who = Item.NewItem(
					position: pos,
					Type: itemEnt.ItemDef.Type,
					Stack: stack
				);

				if( who >= 0 && who < Main.item.Length && Main.item[who].active ) {
					droppedItemIndexes.Add( who );
				}
			}

			return true;
		}
	}
}