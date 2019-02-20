using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MemesAwakened.NPCs.Sock
{
    public class SockBlast : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sock Blast");
        }

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.penetrate = -1;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.timeLeft = 600;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 100);
        }

        public bool playedSound = false;
        public int dontDrawDelay = 2;

        public override void AI()
        {
            if (!playedSound)
            {
                playedSound = true;
                Main.PlaySound(SoundID.Item42, (int)projectile.Center.X, (int)projectile.Center.Y);
            }
            Effects();
            if (projectile.velocity.Length() < 12f)
            {
                projectile.velocity.X *= 1.05f;
                projectile.velocity.Y *= 1.05f;
            }
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
        }

        public virtual void Effects()
        {
            Lighting.AddLight(projectile.Center, ((255 - projectile.alpha) * 0.05f) / 255f, ((255 - projectile.alpha) * 0.25f) / 255f, ((255 - projectile.alpha) * 0f) / 255f);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            dontDrawDelay = Math.Max(0, dontDrawDelay - 1);
            return dontDrawDelay == 0;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType<Buffs.HolySmite>(), 300);
        }
        
        public override void Kill(int timeLeft)
        {
            projectile.timeLeft = 0;
        }

    }
}
