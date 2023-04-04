using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using System.Runtime.Serialization;

namespace FirstMod.Content.Tiles
{
    internal class SkulkOre : ModTile //ModTile contains all the methods needed to create a tile. Make sure it is "ModTile", and not "ModItem"
    {
        public override void SetStaticDefaults()
        {
            TileID.Sets.Ore[Type] = true; 


            Main.tileSolid[Type] = true; 
            Main.tileMergeDirt[Type] = true; 
            Main.tileBlockLight[Type] = true; 
            Main.tileShine[Type] = 900; 
            Main.tileShine2[Type] = true; 
            Main.tileSpelunker[Type] = true; 
            Main.tileOreFinderPriority[Type] = 350; 
            AddMapEntry(new Color(2, 66, 215), CreateMapEntryName());
            DustType = DustID.Tungsten;
            ItemDrop = ModContent.ItemType<Items.Placeables.SkulkOre>();
            HitSound = SoundID.Tink;

            
            MineResist = 1.5f; 
            MinPick = 60; 

            
        }
    }
}
