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
        public SkulkOreGenPass(string name, float weight) : base(name, weight) { } //Every task in World Generation has its own name, we use these names to access their index in the task list.
                                                                                   //Make the name unique to avoid conflicts.
                                                                                   //The weight value is used to determine the total "weight" of the world. This value helps decide the progression of the world creation bar
                                                                                   //during generation.
        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration) //ApplyPass is the abstract method inside GenPass. This is required, and will error if not included
        {
            //The Progress Message is displayed when this GenPass is active. You can call it whatever you want. For larger world Gen tasks, you can use these to help determine where your code got stuck.
            progress.Message = "Drilling Skulk in the ground...";

            //SkulkOre

            int maxToSpawn = (int)(Main.maxTilesX * Main.maxTilesY * 0.00006); //this will be used for each ore spawned, and this value will change.
                                                                    //This gets the total area of the world in Tiles and then multiplies the value by a small number, in this case 0.00006
                                                                    //Small World: 4,200 * 1,200 * 0.00006 = 302.4 (302)
                                                                    //or 6E-05 for 0.00006
            for (int i = 0; i < maxToSpawn; i++)//used to go from a value of 0 to our maxToSpawn value. Ensures spawning of EXACT amount set by maxToSpawn
            { //WorldGen.genRand.Next takes a min and max value. The min cant be greater than the max.

                int x = WorldGen.genRand.Next(100, Main.maxTilesX - 100); //ensure the value is between 0 and Main.maxTilesX - 1. Anything outside of these values for x will throw an error.
                                                                          //This will set our range to the end of the world to the right - 100 tiles.
                int y = WorldGen.genRand.Next((int)WorldGen.worldSurface, Main.maxTilesY - 300); //WorldGen.worldSurface is the Y value of the surface. This is just the surface value used by WorldGen,
                                                                                                 //not the ground at spawn.
                                                                                                 //The size of the underworld and its position vary based on WorldSize. Doing maxTilesY - 300 ensures the ore will be less
                                                                                                 //likely to spawn in the underworld, but not completely 0.

                WorldGen.TileRunner(x, y, WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 5), ModContent.TileType<SkulkOre>()); //takes 5 params: x,y,strength and type. Use our premade x and y values.
                                            //strength will determine the size and number of ores spawned during each step.
                                            //step will determine how many attempts will be made to spawn the ore. In this instance, 2 - 5 times.
                                            //Type is the TileType we want to use. We can use TileID or ModContent.TileType for this.
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
                if(tile.TileType == TileID.SnowBlock || tile.TileType == TileID.IceBlock || tile.TileType == TileID.Slush)
                {
                    WorldGen.TileRunner(x, y, WorldGen.genRand.Next(2, 5), WorldGen.genRand.Next(1, 4), ModContent.TileType<SkulkRareOre>());
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
