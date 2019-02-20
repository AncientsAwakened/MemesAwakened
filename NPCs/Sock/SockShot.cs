using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace MemesAwakened.NPCs.Sock
{
    public class SockShot : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sock Shot");
            Main.projFrames[projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            projectile.width = 36;
            projectile.height = 28;
            projectile.hostile = true;
            projectile.ignoreWater = true;
        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation();

            projectile.frameCounter++;
            if (projectile.frameCounter > 7)
            {
                projectile.frameCounter = 0;
                projectile.frame += 1;
            }
            if (projectile.frame > 2)
            {
                projectile.frame = 0;
            }
        }

        public override void Kill(int timeLeft)
        {
            int dustType = Main.rand.Next(3);
            int pieCut = 20;
            for (int m = 0; m < pieCut; m++)
            {
                dustType = Main.rand.Next(3);
                dustType = mod.DustType<Dusts.HolyDust>();
                int dustID = Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, 0f, 0f, 100, Color.White, 1.6f);
                Main.dust[dustID].velocity = BaseMod.BaseUtility.RotateVector(default(Vector2), new Vector2(8f + Main.rand.Next(6), 0f), MathHelper.Lerp((float)Main.rand.NextDouble(), 0f, 6.28f));
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
            for (int m = 0; m < pieCut; m++)
            {
                dustType = Main.rand.Next(3);
                dustType = mod.DustType<Dusts.HolyDust>();
                int dustID = Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, 0f, 0f, 100, Color.White, 2f);
                Main.dust[dustID].velocity = BaseMod.BaseUtility.RotateVector(default(Vector2), new Vector2(8f + Main.rand.Next(6), 0f), MathHelper.Lerp((float)Main.rand.NextDouble(), 0f, 6.28f));
                Main.dust[dustID].velocity += (projectile.velocity * -0.5f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
            for (int m = 0; m < 15; m++)
            {
                dustType = Main.rand.Next(3);
                dustType = mod.DustType<Dusts.HolyDust>();
                int dustID = Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, 0f, 0f, 100, Color.White, 1.2f);
                Main.dust[dustID].velocity = BaseMod.BaseUtility.RotateVector(default(Vector2), new Vector2(8f + Main.rand.Next(6), 0f), MathHelper.Lerp((float)Main.rand.NextDouble(), 0f, 6.28f));
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
            Projectile.NewProjectile(projectile.Center, new Vector2(0, 0), mod.ProjectileType<Socksplosion>(), projectile.damage, 0, projectile.owner, 0, 0);
            Main.PlaySound(SoundID.Item62, (int)projectile.position.X, (int)projectile.position.Y);
        }
    }
}
