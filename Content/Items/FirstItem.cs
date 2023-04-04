using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;

namespace FirstMod.Content.Items
{
	internal class FirstItem : ModItem
	{

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("razorleaf");
            Tooltip.SetDefault("This is a test item.\nNot sure if this a material or not, but we will figure that out in a bit.\n-Why");
            // This accesses the creative catalog
            // Setting the research number to 100 before it can be fully accessed 
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
            
        }


		public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            
            Item.value = Item.buyPrice(copper: 25);
            Item.maxStack = 999;
        }
	}
}