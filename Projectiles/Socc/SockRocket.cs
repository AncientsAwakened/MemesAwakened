using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace MemesAwakened.Projectiles.Socc
{
    public class SockRocket : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sock Rocket");
		}

		public override void SetDefaults()
		{
			projectile.width = 14;
			projectile.height = 14;
			projectile.penetrate = -1;
			projectile.hostile = false;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.scale = 0.9f;        
		}
		
		public override void AI()
		{
			Vector2 velocity = projectile.velocity;
			
			if (projectile.velocity.X < 0f)
			{
				projectile.spriteDirection = -1;
				projectile.rotation = (float)Math.Atan2((double)(-(double)projectile.velocity.Y), (double)(-(double)projectile.velocity.X)) - 1.57f;
			}
			else
			{
				projectile.spriteDirection = 1;
				projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			}
			
						
			if (projectile.velocity.X != velocity.X)
			{
				projectile.velocity.X = velocity.X * -0.4f;
			}
			if (projectile.velocity.Y != velocity.Y && (double)velocity.Y > 0.7)
			{
				projectile.velocity.Y = velocity.Y * -0.4f;
			}
			
			projectile.localAI[1] += 1f;
			if (projectile.localAI[1] > 6f)
			{
				projectile.alpha = 0;
			}
			else
			{
				projectile.alpha = (int)(255f - 42f * projectile.localAI[1]) + 100;
				if (projectile.alpha > 255)
				{
					projectile.alpha = 255;
				}
			}
			int num3;
			for (int num236 = 0; num236 < 2; num236 = num3 + 1)
			{
				float num237 = 0f;
				float num238 = 0f;
				if (num236 == 1)
				{
					num237 = projectile.velocity.X * 0.5f;
					num238 = projectile.velocity.Y * 0.5f;
				}
				if (projectile.localAI[1] > 9f)
				{
					if (Main.rand.Next(2) == 0)
					{
						int num239 = Dust.NewDust(new Vector2(projectile.position.X + 3f + num237, projectile.position.Y + 3f + num238) - projectile.velocity * 0.5f, projectile.width - 8, projectile.height - 8, mod.DustType<Dusts.HolyDust>(), 0f, 0f, 100, default(Color), 1f);
						Dust dust = Main.dust[num239];
						dust.scale *= 1.4f + (float)Main.rand.Next(10) * 0.1f;
						dust = Main.dust[num239];
						dust.velocity *= 0.2f;
						Main.dust[num239].noGravity = true;
					}
					if (Main.rand.Next(2) == 0)
					{
						int num240 = Dust.NewDust(new Vector2(projectile.position.X + 3f + num237, projectile.position.Y + 3f + num238) - projectile.velocity * 0.5f, projectile.width - 8, projectile.height - 8, mod.DustType<Dusts.HolyDust>(), 0f, 0f, 100, default(Color), 0.5f);
						Main.dust[num240].fadeIn = 0.5f + (float)Main.rand.Next(5) * 0.1f;
						Dust dust = Main.dust[num240];
						dust.velocity *= 0.05f;
					}
				}
				num3 = num236;
			}
			float num241 = projectile.position.X;
			float num242 = projectile.position.Y;
			float num243 = 600f;
			bool flag5 = false;
			projectile.ai[0] += 1f;
			if (projectile.ai[0] > 15f)
			{
				projectile.ai[0] = 15f;
				for (int num244 = 0; num244 < 200; num244 = num3 + 1)
				{
					if (Main.npc[num244].CanBeChasedBy(projectile, false))
					{
						float num245 = Main.npc[num244].position.X + (float)(Main.npc[num244].width / 2);
						float num246 = Main.npc[num244].position.Y + (float)(Main.npc[num244].height / 2);
						float num247 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num245) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num246);
						if (num247 < num243 && Collision.CanHit(projectile.position, projectile.width, projectile.height, Main.npc[num244].position, Main.npc[num244].width, Main.npc[num244].height))
						{
							num243 = num247;
							num241 = num245;
							num242 = num246;
							flag5 = true;
						}
					}
					num3 = num244;
				}
			}
			if (!flag5)
			{
				num241 = projectile.position.X + (float)(projectile.width / 2) + projectile.velocity.X * 100f;
				num242 = projectile.position.Y + (float)(projectile.height / 2) + projectile.velocity.Y * 100f;
			}
			float num248 = 16f;
			Vector2 vector23 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
			float num249 = num241 - vector23.X;
			float num250 = num242 - vector23.Y;
			float num251 = (float)Math.Sqrt((double)(num249 * num249 + num250 * num250));
			num251 = num248 / num251;
			num249 *= num251;
			num250 *= num251;
			projectile.velocity.X = (projectile.velocity.X * 11f + num249) / 12f;
			projectile.velocity.Y = (projectile.velocity.Y * 11f + num250) / 12f;
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			projectile.Kill();
			target.immune[projectile.owner] = 1;
            target.AddBuff(BuffID.Poisoned, 300);
			target.AddBuff(BuffID.Venom, 300);
        }

        public override void Kill(int timeleft)
        {
			Main.PlaySound(SoundID.Item14, projectile.position);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("SRE"), projectile.damage, projectile.knockBack, projectile.owner);
			projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
			projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
			projectile.width = 80;
			projectile.height = 80;
			projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
			projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
			int num3;
			for (int num735 = 0; num735 < 40; num735 = num3 + 1)
			{
				int num736 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.HolyDust>(), 0f, 0f, 100, default(Color), 2f);
				Dust dust = Main.dust[num736];
				dust.velocity *= 3f;
				if (Main.rand.Next(2) == 0)
				{
					Main.dust[num736].scale = 0.5f;
					Main.dust[num736].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
				}
				num3 = num735;
			}
			for (int num737 = 0; num737 < 70; num737 = num3 + 1)
			{
				int num738 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.HolyDust>(), 0f, 0f, 100, default(Color), 3f);
				Main.dust[num738].noGravity = true;
				Dust dust = Main.dust[num738];
				dust.velocity *= 5f;
				num738 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.HolyDust>(), 0f, 0f, 100, default(Color), 2f);
				dust = Main.dust[num738];
				dust.velocity *= 2f;
				num3 = num737;
			}
			for (int num739 = 0; num739 < 3; num739 = num3 + 1)
			{
				float scaleFactor10 = 0.33f;
				if (num739 == 1)
				{
					scaleFactor10 = 0.66f;
				}
				if (num739 == 2)
				{
					scaleFactor10 = 1f;
				}
				int num740 = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Gore gore = Main.gore[num740];
				gore.velocity *= scaleFactor10;
				Gore var_503_19904_cp_0_cp_0 = Main.gore[num740];
				var_503_19904_cp_0_cp_0.velocity.X = var_503_19904_cp_0_cp_0.velocity.X + 1f;
				Gore var_503_19934_cp_0_cp_0 = Main.gore[num740];
				var_503_19934_cp_0_cp_0.velocity.Y = var_503_19934_cp_0_cp_0.velocity.Y + 1f;
				num740 = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				gore = Main.gore[num740];
				gore.velocity *= scaleFactor10;
				Gore var_503_199EE_cp_0_cp_0 = Main.gore[num740];
				var_503_199EE_cp_0_cp_0.velocity.X = var_503_199EE_cp_0_cp_0.velocity.X - 1f;
				Gore var_503_19A1E_cp_0_cp_0 = Main.gore[num740];
				var_503_19A1E_cp_0_cp_0.velocity.Y = var_503_19A1E_cp_0_cp_0.velocity.Y + 1f;
				num740 = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				gore = Main.gore[num740];
				gore.velocity *= scaleFactor10;
				Gore var_503_19AD8_cp_0_cp_0 = Main.gore[num740];
				var_503_19AD8_cp_0_cp_0.velocity.X = var_503_19AD8_cp_0_cp_0.velocity.X + 1f;
				Gore var_503_19B08_cp_0_cp_0 = Main.gore[num740];
				var_503_19B08_cp_0_cp_0.velocity.Y = var_503_19B08_cp_0_cp_0.velocity.Y - 1f;
				num740 = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				gore = Main.gore[num740];
				gore.velocity *= scaleFactor10;
				Gore var_503_19BC2_cp_0_cp_0 = Main.gore[num740];
				var_503_19BC2_cp_0_cp_0.velocity.X = var_503_19BC2_cp_0_cp_0.velocity.X - 1f;
				Gore var_503_19BF2_cp_0_cp_0 = Main.gore[num740];
				var_503_19BF2_cp_0_cp_0.velocity.Y = var_503_19BF2_cp_0_cp_0.velocity.Y - 1f;
				num3 = num739;
			}
			projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
			projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
			projectile.width = 10;
			projectile.height = 10;
			projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
			projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
        }
    }
}
