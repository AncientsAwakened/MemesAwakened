using Terraria.ModLoader;

namespace MemesAwakened
{
    public class MemePlayer : ModPlayer
    {
        public bool PepsiAccessoryPrevious;
        public bool PepsiAccessory;
        public bool PepsiHideVanity;
        public bool PepsiForceVanity;
        public bool PepsiPower;
        public bool HolySmite = false;

        public override void ResetEffects()
        {
            HolySmite = false;
            HolyLaserBlaster.OnUse = false;
            PepsiAccessoryPrevious = PepsiAccessory;
            PepsiAccessory = PepsiHideVanity = PepsiForceVanity = PepsiPower = false;
        }

        public override void UpdateBadLifeRegen()
        {
            int before = player.lifeRegen;
            bool drain = false;

            if (HolySmite)
            {
                drain = true;
                player.lifeRegen -= 30;
            }
        }

        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (HolySmite)
            {
                for (int i = 0; i < 2; i++)
                {
                    int num4 = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width, player.height, mod.DustType<Dusts.HolyDust>(), 0f, -2.5f, 0, default(Color), 1f);
                    Main.dust[num4].alpha = 100;
                    Main.dust[num4].noGravity = true;
                    Main.dust[num4].scale += Main.rand.NextFloat();
                }
                Lighting.AddLight((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f), 0.5f, 0.25f, 0f);
            }
        }

        public override void UpdateVanityAccessories()
        {
            for (int n = 13; n < 18 + player.extraAccessorySlots; n++)
            {
                Item item = player.armor[n];
                if (item.type == mod.ItemType<Items.Vanity.Pepsi.PepsimanCan>())
                {
                    PepsiHideVanity = false;
                    PepsiForceVanity = true;
                }
            }
        }

        public override void UpdateEquips(ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff)
        {
            // Make sure this condition is the same as the condition in the Buff to remove itself. We do this here instead of in ModItem.UpdateAccessory in case we want future upgraded items to set PepsiAccessory
            if (player.townNPCs >= 1 && PepsiAccessory)
            {
                player.AddBuff(mod.BuffType<Pepsi>(), 60, true);
            }
        }

        public override void FrameEffects()
        {
            if ((PepsiPower || PepsiForceVanity) && !PepsiHideVanity)
            {
                player.legs = mod.GetEquipSlot("PepsimanLegs", EquipType.Legs);
                player.body = mod.GetEquipSlot("PepsimanBody", EquipType.Body);
                player.head = mod.GetEquipSlot("PepsimanHead", EquipType.Head);
            }
        }
    }
}