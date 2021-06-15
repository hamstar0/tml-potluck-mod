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

				PotLuckMod.ProcessPotBreak( PotLuckMod.Instance.PotBreakTile );
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

				int tileX = (int)( item.position.X / 16f );
				int tileY = (int)( item.position.Y / 16f );

				if( Math.Abs(tileX - PotLuckMod.Instance.PotBreakTile.x) < 2 && Math.Abs(tileY - PotLuckMod.Instance.PotBreakTile.y) < 2 ) {
					PotLuckMod.PostProcessPotItems( PotLuckMod.Instance.PotBreakTile, new Item[] { item } );
				}
			}
		}
	}
}