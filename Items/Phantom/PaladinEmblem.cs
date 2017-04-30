using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Bluemagic.Items.Phantom
{
	public class PaladinEmblem : ModItem
	{
		public override void SetDefaults()
		{
			item.name = "Paladin Emblem";
			item.width = 22;
			item.height = 14;
			item.maxStack = 20;
			item.rare = 8;
			item.toolTip = "The dungeon is growing cold...";
			item.useStyle = 4;
			item.useAnimation = 45;
			item.useTime = 45;
			item.UseSound = SoundID.Item44;
			item.consumable = true;
		}

		public override bool CanUseItem(Player player)
		{
			if (!NPC.downedPlantBoss || !player.ZoneDungeon)
			{
				return false;
			}
			for (int x = 0; x < 200; x++)
			{
				if (Main.npc[x].active && (Main.npc[x].type == mod.NPCType("PhantomSoul") || Main.npc[x].type == mod.NPCType("Phantom")))
				{
					return false;
				}
			}
			return true;
		}

		public override bool UseItem(Player player)
		{
			if (Main.netMode != 1)
			{
				int npc = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, mod.NPCType("PhantomSoul"), 0);
				Main.npc[npc].target = player.whoAmI;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(MessageID.SyncNPC, -1, -1, "", npc);
				}
			}
			Main.NewText("You feel something cold leeching your life...", 50, 150, 200);
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.HallowedBar, 5);
			recipe.AddIngredient(ItemID.SpectreBar, 4);
			recipe.AddIngredient(ItemID.Ectoplasm, 3);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}