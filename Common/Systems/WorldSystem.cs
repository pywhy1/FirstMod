using Terraria.ModLoader;
using Terraria.WorldBuilding;
using System.Collections.Generic;
using FirstMod.Common.Systems.GenPasses;

namespace FirstMod.Common.Systems
{
    internal class WorldSystem : ModSystem //Modsystem will be used for alot. Such as: World Generation, Boss Downed System, Key bindings, and more!
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight) //ModifyWorldGenTasks will cover the generation when you generate a NEW world.
                                                                                             //It will not generate on an existing world nor generate Hardmode stuff.
                                                                                             
        {
            int shiniesIndex = tasks.FindIndex(t => t.Name.Equals("Shinies")); //when we made the GenPass, we gave it a name. Vanilla tasks also have a name, and this is where we will get
                                                                               //the index from. We are finding the index of "Shinies" in the task list.
                                                                               //https://github.com/tModLoader/tModLoader/wiki/Vanilla-World-Generation-Steps
                                                                               //if index is not found, it will return -1.
            if (shiniesIndex != -1)
            {
                tasks.Insert(shiniesIndex + 1, new SkulkOreGenPass("Skulk Ore pass", 320f)); //in order to get OUR GenPass to work, we have to insert it into the task list.
                                                                                             //DO NOT ADD IT IF ALREADY EXISTS.
                                                                                            //We want to insert it after shinies has been spawned. So we do shiniesIndex + 1.
            }
        }
    }
}
