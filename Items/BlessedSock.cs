using BaseMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using AAMod.NPCs.Bosses.Sock;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MemesAwakened.Items
{
    public class BlessedSock : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blessed Sock");
            Tooltip.SetDefault(@"All hail the mighty silken savior
only usable during the day");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.rare = 11;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.UseSound = SoundID.Item44;
            item.consumable = false;
        }

        public override bool CanUseItem(Player player)
        {
            if (!Main.dayTime)
            {
                if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("The sock flops in your hand", Color.Orange, false);
                return false;
            }
            if (NPC.AnyNPCs(mod.NPCType<Sock>()))
            {
                if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("SOCC WANT SOCK", Color.Orange, false);
                return false;
            }
            return true;
        }

        public override bool UseItem(Player player)
        {
            SpawnBoss(player, "SoccSpawn", "Sock");
            if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("The light of the holy sun shines, bringing SOCC into this world", Color.Orange, false);
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }

        public void SpawnBoss(Player player, string name, string displayName)
        {
            if (Main.netMode != 1)
            {
                int bossType = mod.NPCType(name);
                if (NPC.AnyNPCs(bossType)) { return; }
                int npcID = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, bossType, 0);
                Main.npc[npcID].Center = player.Center - new Vector2(MathHelper.Lerp(-300f, 300f, (float)Main.rand.NextDouble()), 300f);
                Main.npc[npcID].netUpdate2 = true;
            }
        }
        
        public override void UseStyle(Player p) { BaseUseStyle.SetStyleBoss(p, item, true, true); }
        public override bool UseItemFrame(Player p) { BaseUseStyle.SetFrameBoss(p, item); return true; }
    }
}