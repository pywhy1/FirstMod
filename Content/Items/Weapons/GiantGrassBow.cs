using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System;

namespace FirstMod.Content.Items.Weapons
{
    internal class GiantGrassBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Giant Grass Bow"); //display name on the sword
            Tooltip.SetDefault("Leaf + stick = Bow :D\nHuge, big and bulky"); //description
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1; //how many needed for journey mode research
        }
        public override void SetDefaults()
        {
            //hitbox

            Item.width = 32;
            Item.height = 64;

            //use and animation style
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.useAmmo = AmmoID.Arrow;
            Item.shootSpeed = 4f;

            //damage
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 20;
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
                .AddIngredient(ModContent.ItemType<FirstItem>(), 20)//this accesses my FirstItem material.
                .AddRecipeGroup(RecipeGroupID.Wood, 40)
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


        public override bool AltFunctionUse(Player player)
        {
            return true;
        }



        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                var rand = new Random();
                
                int j = 0;
                int k = 0;
                int c = 0;

                




                j = rand.Next(0, 255);
                c = rand.Next(0, 255);
                k = rand.Next(0, 255);
                

                Color Colors = new Color(j, c, k);

                string[] stringArray = new string[] { "Nice shot!", "Fine Shooting!", "Good shot mate!", "Nice Shot!",
                                                        "Good Shot!",
                                                        "That was incredible!",
                                                        "Woo! I love that shot!",
                                                        "Ha ha! That was awesome!",
                                                        "Whooo! You're the man!",
                                                        "Nice job!",
                                                        "Brilliant!",
                                                        "Well done!",
                                                        "Ha ha ha! Nice!",
                                                        "That was amazing!",
                                                        "Good work!",
                                                        "Ha ha ha ha! You're good!",
                                                        "Sweet shot!",
                                                        "Ha ha ha ha! Nice shot!",
                                                        "Whooo! You're incredible!",
                                                        "Ha ha ha ha! Oh my God, wow!",
                                                        "Unbelievable!" };

                var rand1 = new Random();

                int index = rand1.Next(0, stringArray.Length);


                Rectangle playerRect = new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height);
                int text = CombatText.NewText(playerRect, Colors, "" + stringArray[index], true, true);
                int timeLeft = Main.combatText[text].lifeTime;
                
                


            }
            return true;
        }

    }
}