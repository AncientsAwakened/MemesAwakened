using Terraria.ModLoader;

namespace MemesAwakened
{
    class MemesAwakened : Mod
    {
        public MemesAwakened()
        {
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadBackgrounds = true,
                AutoloadSounds = true
            };
            instance = this;
        }

        public override void Load()
        {
            if (!Main.dedServ)
            {
                instance = this;
                AddEquipTexture(null, EquipType.Legs, "N1_Legs", "AAMod/Items/N1/N1_Legs");

                AddEquipTexture(new Items.Vanity.Pepsi.PepsimanHead(), null, EquipType.Head, "PepsimanHead", "MemesAwakened/Items/Pepsi/PepsimanHead");
                AddEquipTexture(new Items.Vanity.Pepsi.PepsimanBody(), null, EquipType.Body, "PepsimanBody", "MemesAwakened/Items/Pepsi/PepsimanBody", "MemesAwakened/Items/Pepsi/PepsimanBody_Arms");
                AddEquipTexture(new Items.Vanity.Pepsi.PepsimanLegs(), null, EquipType.Legs, "PepsimanLegs", "MemesAwakened/Items/Pepsi/PepsimanLegs");

                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/WeAreNumberOne"), ItemType("N1Box"), TileType("N1Box"));
            }
        }

        public override void Unload()
        {
            instance = null;
        }
        public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {
            if (Main.gameMenu)
                return;
            if (priority > MusicPriority.Environment)
                return;
            Player player = Main.LocalPlayer;
            if (!player.active)
                return;
            if (Main.myPlayer != -1 && !Main.gameMenu && Main.LocalPlayer.active)
            {
                if (player.HeldItem.type == ItemType("Sax"))
                {

                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/WeAreNumberOne");

                    priority = MusicPriority.BossHigh;

                    return;
                }
            }
        }

        public override void AddRecipeGroups()
        {
            RecipeGroup group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "Any Iron", new int[]
            {
                ItemID.IronBar,
                ItemID.LeadBar
            });
            RecipeGroup.RegisterGroup("MemesAwakened:Iron", group);
        }
    }
}
