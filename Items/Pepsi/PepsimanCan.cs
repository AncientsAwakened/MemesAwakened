using Terraria;
using Terraria.ModLoader;

namespace MemesAwakened.Items.Pepsi
{
	public class PepsimanCan : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pepsiman Can");
			Tooltip.SetDefault("PEPSIMAAAAAAAAAN");
		}

		public override void SetDefaults()
		{
			item.width = 34;
			item.height = 50;
			item.accessory = true;
			item.rare = 2;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			MemePlayer p = player.GetModPlayer<MemePlayer>();
			p.PepsiAccessory = true;
			if (hideVisual)
			{
				p.PepsiHideVanity = true;
			}
		}
    }

	public class PepsimanHead : EquipTexture
	{
		public override bool DrawHead()
		{
			return false;
		}
	}

	public class PepsimanBody : EquipTexture
	{
		public override bool DrawBody()
		{
			return false;
		}
	}

	public class PepsimanLegs : EquipTexture
	{
		public override bool DrawLegs()
		{
			return false;
		}
	}
}