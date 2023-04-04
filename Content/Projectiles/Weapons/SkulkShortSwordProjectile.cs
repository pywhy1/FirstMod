using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace FirstMod.Content.Projectiles.Weapons
{
    internal class SkulkShortSwordProjectile : ModProjectile //ModProjectile contains everything needed to create a projectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;

            Projectile.friendly = true; //only damages enemies, and not NPCS, like the rotten egg
            Projectile.penetrate = -1; //how many enemies can it go through, -1 breaks on impact
            Projectile.tileCollide = false; //prevents the projectile being destroyed when it hits a tile.
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ownerHitCheck = true; //prevents hits through tiles. like a sword cannot attack something on the other side.
            Projectile.extraUpdates = 1; //determines how many extra times the projectile will update per tick
            Projectile.timeLeft = 300; //shouldnt come into effect at all, but just ensures the projectile gets taken off screen if it is onscreen too long.
            
            Projectile.aiStyle = ProjAIStyleID.ShortSword; //determines how the projectile will function. -1 means all AI will be custom.

        }

        public override void AI()
        {
            base.AI();
            //Mathhelper will contain helpful values like Pi, PiOver2, PiOver4 that will save time.
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 - MathHelper.PiOver4 * Projectile.spriteDirection; // the velocity has an x and y value that needs to be turned into a radian. The velocity vector has a method for this.
            //spriteDirection will be 1 or -1 based on the direction we are facing. If we face west, it will be -1.

            //we have to create 2 local variables to store HALF the width and height of the projectile
            int halfProjWidth = Projectile.width / 2;
            int halfProjHeight = Projectile.height / 2;

            //offset the graphic so it looks like it is in the players hand.
            DrawOriginOffsetX = 0;
            DrawOffsetX = -((32 / 2) - halfProjWidth);

            DrawOriginOffsetY = -((32 / 2) - halfProjHeight);
        }
    }
}