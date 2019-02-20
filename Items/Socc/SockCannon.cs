using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MemesAwakened.Items.Socc
{
    public class SockCannon : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sock Cannon");
			Tooltip.SetDefault("Shoots exploding Sock Rockets");
		}

		public override void SetDefaults()
		{
			item.useStyle = 5;
			item.autoReuse = true;
			item.useAnimation = 10;
			item.useTime = 10;
            item.shoot = mod.ProjectileType("SockRocket");
            item.useAmmo = AmmoID.Rocket;
            item.width = 50;
			item.height = 20;
			item.UseSound = SoundID.Item11;
			item.damage = 100;
			item.shootSpeed = 15f;
			item.noMelee = true;
			item.value = Item.sellPrice(0, 25, 0, 0);
			item.knockBack = 4f;
			item.rare = 8;
			item.ranged = true;
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-4, 0);
		}
		
        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("SockRocket"), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
    }
}
