using Terraria;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;

namespace MemesAwakened.Items.Socc
{
	public class SockBag : ModItem
	{
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Sack");
			Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}

		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.consumable = true;
			item.width = 36;
			item.height = 32;
			item.rare = 8;
			item.expert = true;
			bossBagNPC = mod.NPCType("Sock");
		}

        public override bool CanRightClick()
		{
			return true;
		}

		public override void OpenBossBag(Player player)
		{
            player.QuickSpawnItem(mod.ItemType("HolySock"));
            if (Main.rand.NextFloat() < 0.01f)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
                modPlayer.HMDevArmor();
            }
            string[] lootTable =
                 {
                    "HolyLaserBlaster",
                    "PuppetStaff",
                    "SockCannon",
                    "SockMace"
                };
            int loot = Main.rand.Next(lootTable.Length);
            if (Main.rand.Next(5) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("Sock"), Main.rand.Next(400));
                return;
            }
            player.QuickSpawnItem(mod.ItemType(lootTable[loot]));
            
		}
	}
}