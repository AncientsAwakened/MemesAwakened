using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MemesAwakened.NPCs.Sock
{
    public class SoccDespawn : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radiant Flash");

        }
        public override void SetDefaults()
        {
            npc.width = 100;
            npc.height = 100;
            npc.alpha = 255;
            npc.damage = 0;
            music = MusicID.Boss5;
            npc.lifeMax = 1;
            npc.dontTakeDamage = true;
            npc.noGravity = true;
            npc.aiStyle = -1;
            npc.timeLeft = 10;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }

        public bool Spawned = false;

        public override void AI()
        {
            npc.scale = 1f - npc.alpha / 255f;
            if (npc.alpha <= 0 && !Spawned)
            {
                Spawned = true;
                npc.alpha = 0;
            }
            if (!Spawned)
            {
                npc.alpha -= 10;
            }
            if (Spawned)
            {
                npc.alpha += 15;
                if (npc.alpha >= 255)
                {
                    npc.active = false;
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color dColor)
        {
            Texture2D Tex = Main.npcTexture[npc.type];
            BaseDrawing.DrawTexture(spriteBatch, Tex, 0, npc.position, npc.width, npc.height, npc.scale, 0, 0, 1, new Rectangle(0, 0, Tex.Width, Tex.Height), new Color(dColor.R, dColor.G, dColor.B, npc.alpha), true);
            return false;
        }
    }
}