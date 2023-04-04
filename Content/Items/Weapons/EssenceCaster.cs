using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using FirstMod.Content.Projectiles.Weapons;
using System.Drawing;

namespace FirstMod.Content.Items.Weapons
{
    internal class EssenceCaster : ModItem
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
            
            Item.UseSound = SoundID.Item71;

            Item.shoot = ModContent.ProjectileType<FireEssense>();
            Item.shootSpeed = 1f;

        }
    }
}
