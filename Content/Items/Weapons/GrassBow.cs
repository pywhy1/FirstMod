using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;

namespace FirstMod.Content.Items.Weapons
{
    internal class GrassBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Grass Bow"); //display name on the sword
            Tooltip.SetDefault("Leaf + stick = Bow :D"); //description
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1; //how many needed for journey mode research
        }
        public override void SetDefaults()
        {
            //hitbox

            Item.width = 16;
            Item.height = 32;

            //use and animation style
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 32;
            Item.useAnimation = 32;
            Item.autoReuse = true;
            Item.shoot = 1;
            Item.useAmmo = AmmoID.Arrow;
            Item.shootSpeed = 6f;

            //damage
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 8;
            Item.knockBack = 4f;
            Item.crit = 5;

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
    }
}