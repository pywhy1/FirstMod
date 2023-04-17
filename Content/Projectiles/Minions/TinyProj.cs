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

namespace FirstMod.Content.Projectiles.Minions
{
    internal class TinyProj : ModProjectile

    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;

            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;

            Projectile.DamageType = DamageClass.Summon;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 100;
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


            
            

            Projectile.velocity *= 1.06f;

        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

            target.AddBuff(31, 60 * 2);
            Projectile.Kill();


        }

    }
}


