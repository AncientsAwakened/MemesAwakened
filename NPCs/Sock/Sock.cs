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
	public class Sock : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("S O C C");
            Main.npcFrameCount[npc.type] = 11;			
		}

		public override void SetDefaults()
		{
            npc.aiStyle = -1;
            npc.width = 60;
            npc.height = 140;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.chaseable = true;
            npc.lavaImmune = true;
            npc.boss = true;
            npc.damage = 120;
            npc.defense = 50;
            npc.lifeMax = 60000;
            npc.knockBackResist = 0f;
            npc.npcSlots = 10f;
            npc.HitSound = SoundID.NPCHit14;
            npc.DeathSound = SoundID.NPCDeath20;
            npc.value = 10000f;
            npc.netAlways = true;
            npc.timeLeft = NPC.activeTime * 30;
            music = MusicID.Boss4;
            musicPriority = MusicPriority.BossMedium;
            bossBag = mod.ItemType("SoccBag");
            npc.alpha = 255;
        }

        public float[] customAI = new float[4];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write((short)customAI[0]);
                writer.Write((short)customAI[1]);
                writer.Write((short)customAI[2]);
                writer.Write((short)customAI[3]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                customAI[0] = reader.ReadFloat();
                customAI[1] = reader.ReadFloat();
                customAI[2] = reader.ReadFloat();
                customAI[3] = reader.ReadFloat();
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            
            if (npc.ai[2] > 900)
            {
                if (npc.frameCounter > 6)
                {
                    npc.frameCounter = 0;
                    npc.frameCounter += 150;
                }
                if (npc.frame.Y < 150 * 6 || npc.frame.Y > 150 * 10)
                {
                    npc.frame.Y = 150 * 6;
                }
            }
            else
            {
                if (npc.frameCounter > 10)
                {
                    npc.frameCounter = 0;
                    npc.frameCounter += 150;
                }
                if (npc.frame.Y > 150 * 5)
                {
                    npc.frame.Y = 0;
                }
            }
        }

        int despawnTimer = 0;
        bool Despawn = false;

        public override void AI()
        {
            Player player = Main.player[npc.target];
            
            if (player.Center.X > npc.Center.X)
            {
                npc.spriteDirection = 1;
            }
            else
            {
                npc.spriteDirection = -1;
            }
            npc.ai[2]++;
            if (npc.ai[2] > 600)
            {
                if (npc.ai[2] == 600)
                {
                    npc.ai[3] += 1;
                }
                if (npc.ai[2] < 700)
                {
                    FireMagic(npc, npc.velocity);
                }
                else
                {
                    npc.ai[2] = 0;
                }
            }
            else
            {
                float distanceAmt = 1f;
                npc.TargetClosest(true);
                float distX = Main.player[npc.target].Center.X - npc.Center.X;
                float distY = Main.player[npc.target].Center.Y - npc.Center.Y;
                float dist = (float)Math.Sqrt((double)(distX * distX + distY * distY));
                float maxDistanceAmt = 4f;
                float maxDistance = 250f;
                float increment = 0.040f;
                float closeIncrement = 0.030f;
                npc.ai[1] += 1f;
                if (npc.ai[1] > 280f)
                {
                    increment *= 40;
                    distanceAmt = 4f;
                    if (npc.ai[1] > 330f) { npc.ai[1] = 0f; }
                }
                else
                if (dist < 250f)
                {
                    npc.ai[0] += 0.9f;
                    if (npc.ai[0] > 0f) { npc.velocity.Y = npc.velocity.Y + closeIncrement; } else { npc.velocity.Y = npc.velocity.Y - closeIncrement; }
                    if (npc.ai[0] < -100f || npc.ai[0] > 100f) { npc.velocity.X = npc.velocity.X + closeIncrement; } else { npc.velocity.X = npc.velocity.X - closeIncrement; }
                    if (npc.ai[0] > 200f) { npc.ai[0] = -200f; }
                }
                if (dist > maxDistance)
                {
                    distanceAmt = maxDistanceAmt + (maxDistanceAmt / 4f);
                    increment = 0.3f;
                }
                else
                if (dist > maxDistance - (maxDistance / 7f))
                {
                    distanceAmt = maxDistanceAmt - (maxDistanceAmt / 4f);
                    increment = 0.2f;
                }
                else
                if (dist > maxDistance - (2 * (maxDistance / 7f)))
                {
                    distanceAmt = (maxDistanceAmt / 2.66f);
                    increment = 0.1f;
                }
                dist = distanceAmt / dist;
                distX *= dist; distY *= dist;
                if (Main.player[npc.target].dead)
                {
                    distX = (float)npc.direction * distanceAmt / 2f;
                    distY = -distanceAmt / 2f;
                }
                if (npc.velocity.X < distX) { npc.velocity.X = npc.velocity.X + increment; }
                else
                if (npc.velocity.X > distX) { npc.velocity.X = npc.velocity.X - increment; }
                if (npc.velocity.Y < distY) { npc.velocity.Y = npc.velocity.Y + increment; }
                else
                if (npc.velocity.Y > distY) { npc.velocity.Y = npc.velocity.Y - increment; }
            }

            if (Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000f || Main.player[npc.target].dead)
            {
                npc.velocity *= .8f;
                if (npc.velocity.X < .5f || npc.velocity.X > -.5f)
                {
                    npc.velocity.X = 0;
                }
                if (npc.velocity.Y < .5f || npc.velocity.Y > -.5f)
                {
                    npc.velocity.Y = 0;
                }

                if (npc.velocity.Y == 0 && npc.velocity.X == 0)
                {
                    Vector2 spawnAt = npc.Center + new Vector2(0f, npc.height / 2f);
                    npc.alpha -= 15;
                    if (!Despawn)
                    {
                        Despawn = true;
                        NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, mod.NPCType("SoccDespawn"));
                    }
                    if (npc.alpha >= 255)
                    {
                        npc.active = false;
                    }
                }
            }
        }

        public override void NPCLoot()
        {
            for (int Dust1 = 0; Dust1 < 5; Dust1++)
            {
                int dust1 = mod.DustType<Dusts.HolyDust>();
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust1].velocity *= 0.5f;
                Main.dust[dust1].scale *= 1.3f;
                Main.dust[dust1].fadeIn = 1f;
                Main.dust[dust1].noGravity = false;
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
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
                    npc.DropLoot(mod.ItemType("Sock"), 200, 300);
                    return;
                }
                npc.DropLoot(mod.ItemType(lootTable[loot]));
            }
        }

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {

            Texture2D glowTex = mod.GetTexture("Glowmasks/Sock_Glow");

            if (npc.ai[1] > 280f)
            {
                BaseDrawing.DrawAfterimage(spritebatch, Main.npcTexture[npc.type], 0, npc, 1.5f, 1f, 3, false, 0f, 0f, new Color(dColor.R, dColor.G, dColor.B, 150));
            }

            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc, new Color(dColor.R, dColor.G, dColor.B, npc.alpha));

            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, npc, Color.White, true);
            return false;
        }


        public void FireMagic(NPC npc, Vector2 velocity)
        {

            int ShootThis = -1;
            int Loop;

            Player player = Main.player[npc.target];
            int num429 = 1;
            if (npc.position.X + (npc.width / 2) < Main.player[npc.target].position.X + Main.player[npc.target].width)
            {
                num429 = -1;
            }
            Vector2 PlayerDistance = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
            float PlayerPosX = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) + (num429 * 180) - PlayerDistance.X;
            float PlayerPosY = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - PlayerDistance.Y;
            float PlayerPos = (float)Math.Sqrt((PlayerPosX * PlayerPosX) + (PlayerPosY * PlayerPosY));
            float num433 = 6f;
            PlayerPosX = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - PlayerDistance.X;
            PlayerPosY = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - PlayerDistance.Y;
            PlayerPos = (float)Math.Sqrt((PlayerPosX * PlayerPosX + PlayerPosY * PlayerPosY));
            PlayerPos = num433 / PlayerPos;
            PlayerPosX *= PlayerPos;
            PlayerPosY *= PlayerPos;
            PlayerPosY += Main.rand.Next(-40, 41) * 0.01f;
            PlayerPosX += Main.rand.Next(-40, 41) * 0.01f;
            PlayerPosY += npc.velocity.Y * 0.5f;
            PlayerPosX += npc.velocity.X * 0.5f;
            PlayerDistance.X -= PlayerPosX * 1f;
            PlayerDistance.Y -= PlayerPosY * 1f;
            Vector2 spawnAt = npc.Center + new Vector2(0f, npc.height / 2f);
            if (Main.expertMode)
            {
                Loop = 5;
            }
            else
            {
                Loop = 3;
            }
            if (npc.ai[0] > 25)
            {
                npc.ai[0] = 0;
            }
            if (npc.ai[3] == 1 || npc.ai[3] == 4 || npc.ai[3] == 6 || npc.ai[3] == 15 || npc.ai[3] == 20)
            {
                if (npc.ai[2] == 625 || npc.ai[2] == 650 || npc.ai[2] == 675 || npc.ai[2] == 699)
                {
                    ShootThis = mod.ProjectileType<SockBlast>();
                }
            }
            if ((npc.ai[3] == 2 || npc.ai[3] == 3 || npc.ai[3] == 9 || npc.ai[3] == 17 || npc.ai[3] == 22))
            {
                if (npc.ai[2] == 950)
                {
                    for (int Minions = 0; Minions < Loop; Minions++)
                    {
                        NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, mod.NPCType("SockMinion"), 0, -npc.velocity.X * 1.2f, -npc.velocity.Y * 1.2f);
                    }
                }
                return;
            }
            if (npc.ai[3] == 5 || npc.ai[3] == 12 || npc.ai[3] == 16 || npc.ai[3] == 21 || npc.ai[3] == 25)
            {
                if (npc.ai[2] == 950)
                {

                }
                    ShootThis = mod.ProjectileType<SockonianSun>();
            }
            if (npc.ai[3] == 7 || npc.ai[3] == 11 || npc.ai[3] == 14 || npc.ai[3] == 18 || npc.ai[3] == 24)
            {
                if (npc.ai[2] == 950)
                {
                    ShootThis = mod.ProjectileType<SockShot>();
                }
            }
            if (npc.ai[3] == 8 || npc.ai[3] == 10 || npc.ai[3] == 13 || npc.ai[3] == 19 || npc.ai[3] == 23)
            {
                if (npc.ai[2] == 615 || npc.ai[2] == 630 || npc.ai[2] == 645 || npc.ai[2] == 660 || npc.ai[2] == 675 || npc.ai[2] == 690)
                {
                    ShootThis = mod.ProjectileType<SockLaser>();
                }
            }
            if (ShootThis != -1)
            {
                Projectile.NewProjectile(PlayerDistance.X, PlayerDistance.Y, PlayerPosX * 2, PlayerPosY * 2, ShootThis, (int)(npc.damage * .8f), 0f, Main.myPlayer);
            }
        }
    }
}