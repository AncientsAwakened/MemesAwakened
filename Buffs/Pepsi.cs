using Terraria;
using Terraria.ModLoader;

namespace MemesAwakened.Buffs
{
	public class Pepsi : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Caffine Rush");
			Description.SetDefault("PEPSIMAAAAAAAAN");
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
			canBeCleared = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer p = player.GetModPlayer<MemePlayer>();
            
			if (p.PepsiAccessoryPrevious)
			{
                p.PepsiPower = true;
                player.moveSpeed *= 1.4f;
            }
			else
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
