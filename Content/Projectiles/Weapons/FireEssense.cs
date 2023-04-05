using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using FirstMod.Content.Dusts;

namespace FirstMod.Content.Projectiles.Weapons
{
    internal class FireEssense : ModProjectile

    {
        public override void SetDefaults()
        {
            Projectile.width = 16; 
            Projectile.height = 16;

            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;

            Projectile.DamageType = DamageClass.Magic;
            Projectile.aiStyle = -1; //custom is -1

            Projectile.penetrate = -1;


        }

        public override void AI() //only override AI() method if using original ai style. if using existing or vanilla,
                                  //override PreAI() for before the AI runs or PostAI after the AI runs.
        {
            Projectile.ai[0]++; //Projectile.ai is an array of floats that only contains 2 params.
                            //Projectile.ai[0] is going to be used for the timer
            if (Projectile.ai[0] < 60f)
            {
                //velocity is determined on creation of projectile
                //it is based on cursor pos and Item.shootSpeed variable

                Projectile.velocity *= 1.01f;
                //dont use large numbers when multiplying velocity
                //if you want, you can do it by a large number when the timer is on a specified value
            }
            else
            {
                Projectile.velocity *= 1.05f;
                Projectile.alpha += 1;
                if (Projectile.ai[0] >= 180) //60 frames is 1 second.(assuming no extraUpdates are applied.) 
                {
                    Projectile.Kill();
                    //during the final frames you might want to increase alpha value so it looks like it fades.
                }
            }
            float rotateSpeed = 0.35f * (float)Projectile.direction; //rotates in the direction being faced. useful.
            Projectile.rotation += rotateSpeed;
            //take the rgb value you want and devide by 255. The params here take 0-1 values.
            Lighting.AddLight(Projectile.Center, 1, 0, 0); //makes a light area around the projectile. Not a glow mask.
            
            if(Main.rand.NextBool(2)) //ask chatgpt about this plz
            {
                int numToSpawn = Main.rand.Next(3);
                for(int i = 0; i < numToSpawn; i++) 
                {                                                                           //ItemID.dustType Im sure, or something of the like for vanilla dust.
                    Dust.NewDust(Projectile.position, Projectile.height, Projectile.width, ModContent.DustType<WDust>(), Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f,
                        0, default (Color), 1f); //1f is normal size, 2f is double, 0.5f is half size. determines dust size.
                }
            }

        }

    }
}
