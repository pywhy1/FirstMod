using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using FirstMod.Content.Dusts;
using Terraria.ID;
using FirstMod.Content.Items.Weapons;
using System;
using Microsoft.CodeAnalysis;
using Terraria.GameContent;
using System.Collections.Generic;

namespace FirstMod.Content.Projectiles.Weapons
{
    internal class LiteProjectile : ModProjectile

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
        
        public override void AI()
        {
            
            int PROJ_TYPE = ModContent.ProjectileType<LiteProjectile>();
            int dust = Dust.NewDust(Projectile.Center, 1, 1, DustID.MagicMirror, 0f, 0f, 0, default, 1f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 0.3f;
            Main.dust[dust].scale = Main.rand.Next(100, 135) * 0.013f;

            int dust2 = Dust.NewDust(Projectile.Center, 1, 1, DustID.Shadowflame, 0f, 0f, 0, default, 1f);
            Main.dust[dust2].noGravity = true;
            Main.dust[dust2].velocity *= 0.3f;
            Main.dust[dust2].scale = Main.rand.Next(100, 135) * 0.013f;

            Projectile.ai[0]++; //updates every frame. In this use case, we are setting this to tell us how long the projectile has been in game by frame count.
            

            Player player = Main.player[Projectile.owner];
            Vector2 playerPosition = player.position;

            float maxDetectRadius = 400f; // The maximum radius at which a projectile can detect a target
            float projSpeed = 5f; // The speed at which the projectile moves towards the target

            // Trying to find NPC closest to the projectile
            NPC closestNPC = FindClosestNPC(maxDetectRadius);
            if (closestNPC == null)
                return;

            // If found, change the velocity of the projectile and turn it in the direction of the target
            // Use the SafeNormalize extension method to avoid NaNs returned by Vector2.Normalize when the vector is zero
            Projectile.velocity = (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed;
            Projectile.rotation = Projectile.velocity.ToRotation();






        }
        public NPC FindClosestNPC(float maxDetectDistance)
        {
            NPC closestNPC = null;

            // Using squared values in distance checks will let us skip square root calculations, drastically improving this method's speed.
            float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

            // Loop through all NPCs(max always 200)
            for (int k = 0; k < Main.maxNPCs; k++)
            {
                NPC target = Main.npc[k];
                // Check if NPC able to be targeted. It means that NPC is
                // 1. active (alive)
                // 2. chaseable (e.g. not a cultist archer)
                // 3. max life bigger than 5 (e.g. not a critter)
                // 4. can take damage (e.g. moonlord core after all it's parts are downed)
                // 5. hostile (!friendly)
                // 6. not immortal (e.g. not a target dummy)
                if (target.CanBeChasedBy())
                {
                    // The DistanceSquared function returns a squared distance between 2 points, skipping relatively expensive square root calculations
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);

                    // Check if it is within the radius
                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        sqrMaxDetectDistance = sqrDistanceToTarget;
                        closestNPC = target;
                    }
                }
            }

            return closestNPC;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.Kill();
        }
    }
}
