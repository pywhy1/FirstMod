using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using FirstMod.Content.Dusts;
using System.Security.Cryptography.X509Certificates;
using static Terraria.ModLoader.PlayerDrawLayer;
using Microsoft.Xna.Framework.Input;
using System;
using System.Security.Cryptography;
using IL.Terraria.ID;

namespace FirstMod.Content.Projectiles.Weapons
{
    internal class MiniEssense : ModProjectile

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
        NPC NearestNPC(Vector2 originposition)
        {
            NPC nearestNPC = null;
            float nearestDistance = Projectile.position.X + 100 * 16; // 100 tiles radius
            foreach (NPC npc in Main.npc)
            {
                // Skip any NPCs that are not active or friendly
                if (!npc.active || npc.friendly)
                {
                    continue;
                }

                float distance = Vector2.DistanceSquared(Projectile.position, npc.Center);

                // If this NPC is closer than the current nearest NPC, update the nearest NPC
                if (distance < nearestDistance)
                {
                    nearestNPC = npc;
                    nearestDistance = distance;
                }
            }
            return nearestNPC;
        }
        public override void AI() //only override AI() method if using original ai style. if using existing or vanilla,
                                  //override PreAI() for before the AI runs or PostAI after the AI runs.
        {
            Projectile.ai[0]++;



            //Projectile.ai is an array of floats that only contains 2 params.
            //Projectile.ai[0] is going to be used for the timer
            if (Projectile.ai[0] < 60f * 5f)
            {
                //velocity is determined on creation of projectile
                //it is based on cursor pos and Item.shootSpeed variable

                //dont use large numbers when multiplying velocityz
                //if you want, you can do it by a large number when the timer is on a specified value
                float shootvelocity = 10f;

                NPC closestnpc = NearestNPC(Projectile.position);
                if (closestnpc == null) //if no npc present
                {
                    Projectile.velocity *= 1.07f;
                }
                else
                {
                    Vector2 ProjToNPC = closestnpc.Center - Projectile.Center;
                    Vector2 DirectionNPCToProj = ProjToNPC.SafeNormalize(Vector2.Zero); //safenormalize is for converting vectors into direction
                    Projectile.velocity = DirectionNPCToProj * shootvelocity;  //normalize is for converting vectors into direction
                }

            }
            else
            {
                //Projectile.velocity *= 1.05f;
                Projectile.alpha += 1;
                //60 frames is 1 second.(assuming no extraUpdates are applied.) 

                Projectile.Kill();
                //during the final frames you might want to increase alpha value so it looks like it fades.

            }
            float rotateSpeed = 0.35f * (float)Projectile.direction; //rotates in the direction being faced. useful.
            Projectile.rotation += rotateSpeed;
            //take the rgb value you want and devide by 255. The params here take 0-1 values.
            Lighting.AddLight(Projectile.Center, 1, 0, 0); //makes a light area around the projectile. Not a glow mask.

            

        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(31, 5 * 60); //buff id, time
            target.StrikeNPC(20, knockback, Projectile.direction, crit);
            Projectile.Kill();
        }

    }
}
