using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.ID;
using FirstMod.Content.Projectiles.Weapons; //allows for use of the projectile in this file


namespace FirstMod.Content.Items.Weapons
{
    internal class SkulkShortSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;

            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Rapier;

            Item.UseSound = SoundID.Item1; //plays sound on use

            Item.DamageType = DamageClass.Melee;
            Item.damage = 20;
            Item.knockBack = 10f;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(silver: 48);

            Item.noUseGraphic = true; //this will prevent the item texure from being shown on use. This is used often when projectiles is used as the weapon
            Item.noMelee = true; // this prevents the item from hurting enemies, this is done also when projectiles are used as the weapon. Example: a yoyo, a melee, but ranged projectile weapon.
            
            Item.shootSpeed = 2.1f;
            Item.shoot = ModContent.ProjectileType<SkulkShortSwordProjectile>(); //item.shoot is the ID of the projectile that is going to be created on use. We use a custom projectile here.


        }   
    }
}