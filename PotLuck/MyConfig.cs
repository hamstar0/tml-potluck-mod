using System;
using System.Collections.Generic;
using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using ModLibsCore.Classes.UI.ModConfig;


namespace PotLuck {
	class MyFloatInputElement : FloatInputElement { }




	public class PotItemEntry {
		[Range( -999, 999 )]
		[DefaultValue(1)]
		public int MinStack;

		[Range( 1, 999 )]
		[DefaultValue( 1 )]
		public int MaxStack;

		public ItemDefinition ItemDef;
	}




	public class PotEntry {
		[Range( 0f, 1f )]
		[DefaultValue( 1f )]
		public float PercentChance;

		[Range( -1, 100 )]
		[DefaultValue( -1 )]
		public int RandomItemPicks = -1;

		//

		[DefaultValue( false )]
		public bool HardModeOnly;

		[DefaultValue( true )]
		public bool IsSurface;

		[DefaultValue( true )]
		public bool IsCaves;

		[DefaultValue( true )]
		public bool IsUnderworld;

		public List<PotItemEntry> ItemDefs = new List<PotItemEntry>();
	}




	public partial class PotLuckConfig : ModConfig {
		public static PotLuckConfig Instance => ModContent.GetInstance<PotLuckConfig>();



		////////////////

		public override ConfigScope Mode => ConfigScope.ServerSide;


		////////////////

		public List<PotEntry> PotEntries = new List<PotEntry>();	/*{
			new PotEntry {
				PercentChance = 1f,
				HardModeOnly = false,
				IsSurface = true,
				IsCaves = true,
				IsUnderworld = true,
				ItemDefs = new List<PotItemEntry> {
					new PotItemEntry {
						MinStack = 1,
						MaxStack = 999,
						ItemDef = new ItemDefinition( ItemID.DirtBlock )
					}
				}
			}
		};*/
	}
}
