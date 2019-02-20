using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MemesAwakened.Items.Socc
{
	public class Sock : ModItem
	{
		public override void SetDefaults()
		{
			item.damage = 100;
			item.thrown = true;
			item.width = 20;
			item.height = 18;
			item.noUseGraphic = true;
			item.maxStack = 999;
			item.consumable = true;
			item.useTime = 30;
			item.useAnimation = 30;
			item.shoot = mod.ProjectileType("SockP");
			item.shootSpeed = 16f;
			item.useStyle = 1;
			item.knockBack = 2;
			item.value = Item.sellPrice(0, 25, 0, 0);
			item.rare = 8;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sock");
			Tooltip.SetDefault("It feels pretty solid\nCritical mass is reached on enemy hit");
		}
	}
}
