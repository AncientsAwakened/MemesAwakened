using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace MemesAwakened.NPCs.Sock
{
	public class SockMinion : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Small Sock");
            Main.npcFrameCount[npc.type] = 8;			
		}

		public override void SetDefaults()
		{
            npc.width = 40;
            npc.height = 40;
            npc.aiStyle = 0;
            npc.damage = 50;
            npc.defense = 34;
            npc.lifeMax = 700;
            npc.HitSound = SoundID.NPCHit6;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.2f;
            npc.buffImmune[31] = false;
        }

        float shootAI = 0;
        public override void AI()
        {
            BaseAI.AISkull(npc, ref npc.ai, false, 6f, 350f, 0.1f, 0.15f);
            Player player = Main.player[npc.target];
            bool playerActive = player != null && player.active && !player.dead;
            BaseAI.Look(npc, ref npc.rotation, ref npc.spriteDirection, 1, 0, 0, true);
            if (Main.netMode != 1 && playerActive)
            {
                shootAI++;
                if (shootAI >= 120)
                {
                    shootAI = 0;
                    int projType = mod.ProjType("SockLaser");
                    if (Collision.CanHit(npc.position, npc.width, npc.height, player.position, player.width, player.height))
                        BaseAI.FireProjectile(player.Center, npc, projType, (int)(npc.damage * 0.25f), 0f, 2f);
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter > 10)
            {
                npc.frame.Y += 64;
                npc.frameCounter = 0;
            }
            if (shootAI < 80 && npc.frame.Y > 64 * 3)
            {
                npc.frame.Y = 0;
            }
            else if (shootAI >= 80 && (npc.frame.Y < 64 * 4 || npc.frame.Y > 64 * 7)) ;
            {
                npc.frame.Y = 64 * 4;
            }
        }
    }
}