using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace MemesAwakened.Items.Socc
{
	public class HolyLaserBlaster : ModItem
	{
		public static bool OnUse = false;
		public static int time = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Holy Laser Blaster");
			Tooltip.SetDefault("Shoots barrage of Holy Lasers");
        }

		public override void SetDefaults()
		{
			item.damage = 75;
			item.magic = true;
			item.mana = 7;
			item.knockBack = 4;
			item.width = 56;
			item.height = 26;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = 5;
			item.noMelee = true;
			item.value = 100000;
			item.rare = 8;
			item.autoReuse = true;
			item.shoot = 449;
			item.shootSpeed = 16f;
			item.UseSound = SoundID.Item91;
		}

		public override void UseStyle(Player player)
		{
			OnUse = true;
			item.useTime = 25;
			item.useAnimation = 25;
			time++;
			if (time >= 90)
			{
				item.useTime = 6;
				item.useAnimation = 6;
			}
			else if (time >= 60)
			{
				item.useTime = 12;
				item.useAnimation = 12;
			}
			else if (time >= 30)
			{
				item.useTime = 18;
				item.useAnimation = 18;
			}
		}

		public override void HoldItem(Player player)
		{
			if (OnUse == false)
			{
				time = 0;
			}
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-8, 0);
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles = 3 + Main.rand.Next(1);
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(8));
				int p = Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
				Main.projectile[p].friendly = true;
				Main.projectile[p].hostile = false;
				Main.projectile[p].magic = true;
			}
			return false;
		}
	}
}
