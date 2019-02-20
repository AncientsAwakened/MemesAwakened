using Terraria;
using Terraria.ModLoader;

namespace MemesAwakened.Buffs
{
    public class HolySmite : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Holy Smite");
			Description.SetDefault("The flames of a holy god smite thee");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<AAPlayer>(mod).HolySmite = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<AAModGlobalNPC>(mod).HolySmite = true;
		}
	}
}
