using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using FirstMod.Content.Projectiles.Weapons;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using IL.Terraria.GameContent;
using System;
using System.Threading;
namespace FirstMod.Content.Items.Weapons
{
    internal class SoulOfLite : ModItem
    {
        
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.useStyle = ItemUseStyleID.Shoot; //guns staves and books
            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            Item.mana = 10; //how much mana used
            Item.damage = 20;
            Item.knockBack = 3.2f;

            Item.useTime = 20; //if the useanim is greater than usetime the projectile wil be created multiple times
            Item.useAnimation = 15; //example: if usetime is 15 and anim is 20: you will make a projectile at frame 0 and 15

            Item.UseSound = SoundID.Item109;

            Item.shoot = ModContent.ProjectileType<LiteProjectile>();
            Item.shootSpeed = 1f;


        }
        public override bool AltFunctionUse(Player player)
        {

            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            
            int rapidFireTimer = 0;
            
            Vector2 pos = player.position;
            if (player.altFunctionUse == 2) // use the item up to 10 times
            {
                while (rapidFireTimer > 10)
                {
                    Projectile.NewProjectile(Projectile.InheritSource(player), pos, velocity, ModContent.ProjectileType<LiteProjectile>(), damage, knockback, player.whoAmI);
                    
                    rapidFireTimer++;
                }
            }
            else
            {
                Projectile.NewProjectile(Projectile.InheritSource(player), pos, velocity, ModContent.ProjectileType<LiteProjectile>(), damage, knockback, player.whoAmI);
            }

            return true;
        }
    }
}
