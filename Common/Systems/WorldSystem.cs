using Terraria.ModLoader;
using Terraria.WorldBuilding;
using System.Collections.Generic;
using FirstMod.Common.Systems.GenPasses;

namespace FirstMod.Common.Systems
{
    internal class WorldSystem : ModSystem //Modsystem will be used for alot. Such as: World Generation, Boss Downed System, Key bindings, and more!
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight) 
                                                                                             
        {
            int shiniesIndex = tasks.FindIndex(t => t.Name.Equals("Shinies"));

            if (shiniesIndex != -1)
            {
                tasks.Insert(shiniesIndex + 1, new SkulkOreGenPass("Skulk Ore pass", 320f)); 
            }
        }
    }
}
