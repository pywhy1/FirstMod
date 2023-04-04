using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using IL.Terraria.ObjectData;
using System.Security.Cryptography.X509Certificates;

namespace FirstMod.Content.Items.Placeables
{
    internal class SkulkRareBar : ModItem
    {
        public override void SetStaticDefaults() 
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 10;
            ItemID.Sets.SortingPriorityMaterials[Type] = 49;
        }
        public override void SetDefaults() 
        {
            Item.width = 20;
            Item.height = 20;

            Item.maxStack = 999;
            Item.consumable = true;
            Item.value = Item.buyPrice(gold: 2);

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useTurn = true;
            Item.autoReuse = true;

            Item.createTile = ModContent.TileType<Tiles.ModBars>();
            Item.placeStyle = 1;

            
            
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<SkulkOre>(3)
                .AddTile(TileID.Furnaces)
                .Register();
        }
    }
}
