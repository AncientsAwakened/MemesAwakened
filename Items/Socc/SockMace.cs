using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Localization;
using Terraria.World.Generation;

namespace MemesAwakened.Items.Socc
{
	public class SockMace : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Who may thought that sock + soap may be so hurtful combination?\nCan stun non-boss enemies on hit");
        }

		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.Muramasa);
			item.melee = true;
			item.damage = 150;
			item.crit = 10;
			item.width = 40;
			item.height = 40;
			item.useTime = 25;
			item.useAnimation = 25;
			item.hammer = 100;
			item.value = 100000;
			item.rare = 8;
            item.knockBack = 10;
            item.autoReuse = true;
			item.UseSound = SoundID.Item1;
		}
		
		public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
		{
			damage *= 2;
			if (target.boss == false && target.lifeMax <= 1500)
			{
				target.AddBuff(mod.BuffType("Stunned"), 60);
			}
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(2) == 0)
			{
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 46);
			}
		}	
	}
}
