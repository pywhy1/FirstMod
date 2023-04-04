using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;

namespace FirstMod.Content.Items.Weapons
{
    internal class GrassBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Grass Bow"); //display name on the sword
            Tooltip.SetDefault("Leaf + stick = Bow :D\nNimble and fast."); //description
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1; //how many needed for journey mode research
        }
        public override void SetDefaults()
        {
            //hitbox

            Item.width = 16;
            Item.height = 32;

            //use and animation style
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.autoReuse = true;
            Item.shoot = 1;
            Item.useAmmo = AmmoID.Arrow;
            Item.shootSpeed = 9f;

            //damage
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 6;
            Item.knockBack = 6f;
            Item.crit = 10;

            //misc
            Item.value = Item.buyPrice(gold: 3);
            Item.rare = ItemRarityID.Blue; //check the wiki for these values

            //sound
            Item.UseSound = SoundID.Item5;
        }

        public override void AddRecipes()
        {
            CreateRecipe() //this takes a param in the '()' for how many gets created at a time, by default it is 1.
                .AddIngredient(ModContent.ItemType<FirstItem>(), 6)//this accesses my FirstItem material.
                .AddRecipeGroup(RecipeGroupID.Wood, 10)
                //.AddIngredient(ItemID.Wood, 10) We use recipe group so I can use any wood in the crafting recipe
                .AddTile(TileID.WorkBenches) //addtile takes tile id for crafting (like if it takes a anvil or a work bench)
                .Register(); //register registers the item.

        }

        public override Vector2? HoldoutOffset() //this will change where our item is drawn, for looks purposes
        {

            //In terraria, pixels are scaled by 2, so if we want to move 2 whole pixels in game, we have to specify 2 * specified amount, so four in this case

            //Also, the default offset is 10, so for whatever reason, instead of 4, we would use 6
            Vector2 offset = new Vector2(6,0);

            return offset;
        }
    }
}