using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Utilities;
using HamstarHelpers.Helpers.Items;
using HamstarHelpers.Helpers.TModLoader;
using HamstarHelpers.Helpers.World;


namespace PotLuck {
	public partial class PotLuckMod : Mod {
		internal static void ProcessPotBreak( (int x, int y) potTile ) {
			foreach( Func<(int, int), Item[]> action in PotLuckMod.Instance.OnPotBreakActions ) {
				PotLuckMod.PostProcessPotItems( potTile, action(potTile) );
			}

			var potEntries = PotLuckConfig.Instance.Get<List<PotEntry>>( nameof(PotLuckConfig.PotEntries) );
			foreach( PotEntry potEnt in potEntries ) {
				PotLuckMod.CheckPotEntry( potEnt, potTile );
			}
		}

		internal static void PostProcessPotItems( (int x, int y) potTile, Item[] items ) {
			foreach( Action<(int, int), Item[]> action in PotLuckMod.Instance.OnPotBreakItemDropActions ) {
				action( potTile, items );
			}
		}


		////

		private static void CheckPotEntry( PotEntry potEnt, (int x, int y) potTile ) {
			if( potEnt.HardModeOnly && !Main.hardMode ) {
				return;
			}
			if( !potEnt.IsSurface && potTile.y < WorldHelpers.SurfaceLayerBottomTileY ) {
				return;
			}
			if( !potEnt.IsCaves && potTile.y >= WorldHelpers.SurfaceLayerBottomTileY && potTile.y < WorldHelpers.UnderworldLayerTopTileY ) {
				return;
			}
			if( !potEnt.IsUnderworld && potTile.y >= WorldHelpers.UnderworldLayerTopTileY ) {
				return;
			}

			UnifiedRandom rand = TmlHelpers.SafelyGetRand();
			if( rand.NextFloat() > potEnt.PercentChance ) {
				return;
			}

			var items = new List<Item>();

			foreach( PotItemEntry itemEnt in potEnt.ItemDefs ) {
				var pos = new Vector2( potTile.x<<4, potTile.y<<4 );
				int stack = rand.Next( itemEnt.MinStack, itemEnt.MaxStack );

				int who = ItemHelpers.CreateItem( pos, itemEnt.ItemDef.Type, stack, 16, 16 );

				if( who >= 0 && who < Main.item.Length && Main.item[who].active ) {
					items.Add( Main.item[who] );
				}
			}

			PotLuckMod.PostProcessPotItems( potTile, items.ToArray() );
		}
	}
}