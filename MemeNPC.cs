using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace MemesAwakened
{
    public class AAModGlobalNPC : GlobalNPC
    {

        public bool HolySmite = false;

        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {

            int before = npc.lifeRegen;
            bool drain = false;
            bool noDamage = damage <= 1;
            int damageBefore = damage;
            int num = npc.lifeRegenExpectedLossPerSecond;

            if (HolySmite)
            {
                drain = true;
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }

                npc.lifeRegen -= 30;
            }
        }
        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            Rectangle hitbox = npc.Hitbox;
            if (HolySmite)
            {
                for (int i = 0; i < 2; i++)
                {
                    int num4 = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width, npc.height, mod.DustType<Dusts.HolyDust>(), 0f, -2.5f, 0, default(Color), 1f);
                    Main.dust[num4].alpha = 100;
                    Main.dust[num4].noGravity = true;
                    Main.dust[num4].scale += Main.rand.NextFloat();
                }
                Lighting.AddLight((int)(npc.Center.X / 16f), (int)(npc.Center.Y / 16f), 0.5f, 0.25f, 0f);
            }

        }
        public override void SetupShop(int type, Chest shop, ref int nextSlot)
		{
            if (type == NPCID.Clothier)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType<Items.Vanity.Pepsi.PepsimanCan>());
                nextSlot++;
                if (Main.hardMode)
                {
                    shop.item[nextSlot].SetDefaults(mod.ItemType("BlessedSock"));
                    nextSlot++;
                }
            }
        }
    }
}
