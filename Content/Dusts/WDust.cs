using IL.Terraria.GameContent.ObjectInteractions;
using System.Diagnostics;
using Terraria;
using Terraria.ModLoader;

namespace FirstMod.Content.Dusts
{
    internal class WDust : ModDust
    {
        public override void OnSpawn(Dust dust) //the OnSpawn method will allow for seeing dust properties when the dust is created.
                                                //you will not want to modify the values that we set when we created the dust
        {
            dust.noGravity = true; //prevents dust from floating down to the ground
            dust.noLight = true; //if no light is desired from the dust, or a custom lighting effect will be used, then set this to true.

        }

        public override bool Update(Dust dust) //The Update method is similar to AI method for Projectiles.
                                               //It handles how the dust will move around, rotate, scale and more each frame.
        {
            dust.position += dust.velocity; //Because we are writing this Update method completely custom, we need to update the dusts position based on the velocity.
            dust.rotation += dust.velocity.X * 0.15f;
            dust.scale *= 0.99f; //slowly become smaller and smaller
            if(dust.scale < 0.3f) 
            {
                dust.active = false;

            }
            Lighting.AddLight(dust.position, 1, 0, 0);

            return false; //The update method returns a boolean. Becasue we dont want the vanilla update code to run, we return false.
        }
    }
}
