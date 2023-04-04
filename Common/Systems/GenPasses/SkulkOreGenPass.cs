using Terraria;
using Terraria.ModLoader;
using Terraria.IO;
using Terraria.WorldBuilding;
using FirstMod.Content.Tiles; //use this to access ore tiles for generation
using Terraria.ID;

namespace FirstMod.Common.Systems.GenPasses
{
    internal class SkulkOreGenPass : GenPass//A GenPass is a class that is called during world generation. They contain the code needed to generate their dedicated item.
                                    //For example, the Dungeon has its own GenPass that is used and contains the code for that.
                                    //The GenPass class contains an abstract method. This means that when we extend GenPass we MUST have this method. GenPass also has parameters in its
                                    //constructor and we need to set these values in our own constructor
    {
        public SkulkOreGenPass(string name, float weight) : base(name, weight) { } 
                                                                                   
        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration) 
        {
            
            progress.Message = "Drilling Skulk in the ground...";

            //SkulkOre

            int maxToSpawn = (int)(Main.maxTilesX * Main.maxTilesY * 0.00006);
            for (int i = 0; i < maxToSpawn; i++)
            { 

                int x = WorldGen.genRand.Next(100, Main.maxTilesX - 100); 
                int y = WorldGen.genRand.Next((int)WorldGen.worldSurface, Main.maxTilesY - 300); 

                WorldGen.TileRunner(x, y, WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 5), ModContent.TileType<SkulkOre>()); 
                                           
            }

            //SkulkRareOre
            maxToSpawn = WorldGen.genRand.Next(100, 250);
            int numSpawned = 0;
            int attempts = 0;
            while (numSpawned < maxToSpawn)
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX); //this is just to get a random point in the whole world
                int y = WorldGen.genRand.Next(0, Main.maxTilesY);

                Tile tile = Framing.GetTileSafely(x, y);//get the tile at the current x and y position as we only want the ore the spawn in a specific location, do this by checking the TileType.
                if (tile.TileType == TileID.SnowBlock || tile.TileType == TileID.IceBlock || tile.TileType == TileID.Slush || tile.TileType == TileID.Mud)
                {
                    WorldGen.TileRunner(x, y, WorldGen.genRand.Next(6, 10), WorldGen.genRand.Next(7, 10), ModContent.TileType<SkulkRareOre>());
                    numSpawned++; 
                }

                attempts++;
                if (attempts >= 100000) //safety so we dont end up in an infinite loop
                {
                    break;
                }    
            }
        }
    }
}
