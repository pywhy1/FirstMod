using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using FirstMod.Content.Projectiles.Weapons;

namespace FirstMod.Content.Items.Weapons
{
    internal class FirstSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("RazorLeaf Sword"); //display name on the sword
            Tooltip.SetDefault("Created with the sharp edges of nature."); //description
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1; //how many needed for journey mode research
        }
        public override void SetDefaults()
        {
            //hitbox

            Item.width = 160;
            Item.height = 160;
            //use and animation style
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 40; 
            Item.useAnimation = 10;
            Item.autoReuse = true;

            //damage
            Item.DamageType = DamageClass.Melee; 
            Item.damage = 100;
            Item.knockBack = 10f; 
            Item.crit = 10; 

            //misc
            Item.value = Item.buyPrice(gold: 20);
            Item.rare = ItemRarityID.Green; //check the wiki for these values

            //sound
            Item.UseSound = SoundID.Item1;
        }

        public override void AddRecipes()
        {
            CreateRecipe() //this takes a param in the '()' for how many gets created at a time, by default it is 1.
                .AddIngredient(ModContent.ItemType<FirstItem>(), 8) //this accesses my FirstItem material.
                .AddTile(TileID.WorkBenches) //addtile takes tile id for crafting (like if it takes a anvil or a work bench)
                .Register(); //register registers the item.

        }
        
    }
}