using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;

namespace FirstMod.Content.Items.Tools
{
    internal class SkulkPickaxe : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;

            Item.useTime = 8;
            Item.useAnimation = 15;

            Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;

            Item.DamageType = DamageClass.Melee;
            Item.damage = 6;
            Item.knockBack = 4f;

            Item.value = Item.buyPrice(silver: 99);
            Item.rare = ItemRarityID.Blue;

            Item.pick = 100; //this is the pickaxe power definition

        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(RecipeGroupID.Wood, 10) //recipe groups are items that are related in some kind of way, like iron and lead, and all wood types.
                .AddIngredient(ModContent.ItemType<FirstItem>(), 4)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}