using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace MemesAwakened.Items.Socc
{
    [AutoloadEquip(EquipType.Shoes)]
    public class HolySock : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Holy Socks");
            Tooltip.SetDefault(@"A pair of blessed cotton socks
80% increased movement speed
Doubles run speed
Grants a speedy dash");
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 34;
            item.maxStack = 1;
            item.value = 10000;
            item.rare = 8;
            item.expert = true;
            item.accessory = true;
        }


        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, Color.DarkMagenta.ToVector3() * 0.55f * Main.essScale);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
            player.moveSpeed *= 1.80f;
            player.dash = 1;
            player.maxRunSpeed *= 2f;
        }
    }
}