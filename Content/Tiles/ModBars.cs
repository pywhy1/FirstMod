using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ObjectData;
using Terraria.Localization;

namespace FirstMod.Content.Tiles
{
    internal class ModBars : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileShine[Type] = 1100;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);

            TileObjectData.newTile.StyleHorizontal = true; 
            TileObjectData.newTile.LavaDeath = false; 
            TileObjectData.addTile(Type); 

            AddMapEntry(new Color(200, 200, 200), Language.GetText("MapObject.MetalBar"));
        }

        public override bool Drop(int x, int y)
        { //We want to get the tile from its position in the world, to do this we create a local variable with the type of Tile. We then set this tile to be equal to whatever
            //tile is in the x and y tiles array
            Tile t = Main.tile[x, y];

            //we need to get the tiles frame index in order to determine what item is going to be dropped. We know that each frame is 18 piels wide so we take the tiles FrameX and divide that by
            //18 to get the frame index.
            int style = t.TileFrameX / 18;
            //the switch statement will make it easier to create the items. We can create a new item depending on the value "Style".
            switch(style) //style, being of course, which bar from the sprite strip we are using.
            {
                //in order to spawn an item, Item.NewItem is used, which takes: Source, X, Y, Width, Height, ItemType. as parameters
                //the x and y value are currently equal to the position of the tile in the array and not the actual position in the world, so to fix this we multiply x and y values by 16.
                //The width and height also must be set to 16.
                //Finally, we have to determine what item is being dropped (again I know), and to do this we pass in the ItemID. We are using a modded item so we use ModContent.ItemType to get that
                //new ID.
                //This will only drop 1 of the item. For multiple of the saMe item add a final parameter with the number you want to drop. The "Stack" parameter is defaulted to 1.
                case 0: Item.NewItem(new EntitySource_TileBreak(x, y), x * 16, y * 16, 16, 16, ModContent.ItemType<Items.Placeables.SkulkBar>()); break;
                case 1: Item.NewItem(new EntitySource_TileBreak(x, y), x * 16, y * 16, 16, 16, ModContent.ItemType<Items.Placeables.SkulkRareBar>()); break;
                case 2:
                    //ADD THE ITEM
                    break;
            }

            return base.Drop(x, y);

        }
    }
}
