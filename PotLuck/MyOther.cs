using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;


namespace PotLuck {
	class PotLuckTile : GlobalTile {
		public override void KillTile( int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem ) {
			if( type != TileID.Pots ) {
				return;
			}

			if( (Main.tile[i,j].frameX % 36) == 0 && (Main.tile[i,j].frameY % 36) == 0 ) {
//LogLibraries.Log("pot break at "+i+", "+j);
//Main.NewText("pot break at "+i+", "+j);
				PotLuckMod.Instance.PotBreakTile = (i, j);

				PotLuckMod.ProcessPotBreak( i, j );
			}
		}
	}




	class PotLuckItem : GlobalItem {
		private bool IsChecked = false;

		////////////////

		public override bool CloneNewInstances => false;
		public override bool InstancePerEntity => true;



		////////////////

		public override GlobalItem Clone( Item item, Item itemClone ) {
			return base.Clone( item, itemClone );
		}

		////////////////

		public override void Update( Item item, ref float gravity, ref float maxFallSpeed ) {
			if( !this.IsChecked ) {
				this.IsChecked = true;

				int itemWho = Array.IndexOf( Main.item, item );
				if( itemWho == -1 ) {
					return;
				}

				int tileX = (int)( item.position.X / 16f );
				int tileY = (int)( item.position.Y / 16f );
				bool isPotNearX = Math.Abs( tileX - PotLuckMod.Instance.PotBreakTile.x ) < 2;
				bool isPotNearY = Math.Abs( tileY - PotLuckMod.Instance.PotBreakTile.y ) < 2;

				if( isPotNearX && isPotNearY ) {
					PotLuckMod.PostProcessPotItems(
						tileX: PotLuckMod.Instance.PotBreakTile.x,
						tileY: PotLuckMod.Instance.PotBreakTile.y,
						droppedItemIndexes: new int[] { itemWho }
					);
				}
			}
		}
	}
}