using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MemesAwakened.Projectiles.Socc
{
	public class SRE : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dummy Explosion");
		}

		public override void SetDefaults()
		{
			projectile.ranged = true;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.width = 128;
			projectile.height = 128;
			projectile.penetrate = -1;
			projectile.timeLeft = 1;
			projectile.tileCollide = false;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[projectile.owner] = 1;
		}
	}
}
