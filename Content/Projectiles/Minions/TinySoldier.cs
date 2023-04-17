using FirstMod.Content.Items;
using Microsoft.Xna.Framework;
using System;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace FirstMod.Content.Projectiles.Minions
{


    public class TinyBuff : ModBuff
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tiny Soldier");
            Description.SetDefault("Tiny's army has chosen you as their leader");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<TinySoldier>()] > 0)
            {
                player.buffTime[buffIndex] = 18000;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }

    public class TinySoldier : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // Denotes that this projectile is a pet or minion
            Main.projPet[Projectile.type] = true;
            // This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            Main.projFrames[Projectile.type] = 16;
        }
        private Color ColorFromHSV(float hue, float saturation, float value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue * 6)) % 6;
            float f = hue * 6 - (float)Math.Floor(hue * 6);
            float p = value * (1 - saturation);
            float q = value * (1 - f * saturation);
            float t = value * (1 - (1 - f) * saturation);

            switch (hi)
            {
                case 0:
                    return new Color(value, t, p);
                case 1:
                    return new Color(q, value, p);
                case 2:
                    return new Color(p, value, t);
                case 3:
                    return new Color(p, q, value);
                case 4:
                    return new Color(t, p, value);
                default:
                    return new Color(value, p, q);
            }
        }
        NPC nearestNPC(float range)
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc != null && npc.CanBeChasedBy())
                {
                    return npc;
                }
            }
            return null;
        }
        
        public override void AI()
        {
            
            float spacing = (float)Projectile.width * 1f;
            // set variables and defaults
            Player player = Main.player[Projectile.owner];
            Vector2 start = Projectile.Center;
            Vector2 idlepos = player.Center;

            idlepos.Y -= 48f;

            float minionPositionOffsetX = (10 + Projectile.minionPos * 40) * -player.direction;
            idlepos.X += minionPositionOffsetX;

            Vector2 vectorToIdlePosition = idlepos - Projectile.Center;
            float distanceToIdlePosition = vectorToIdlePosition.Length();

            float distanceFromTarget = 700f;
            Vector2 targetCenter = Projectile.position;
            bool foundTarget = false;


            #region Active check
            // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
            if (player.dead || !player.active)
            {
                player.ClearBuff(ModContent.BuffType<TinyBuff>());
            }
            if (player.HasBuff(ModContent.BuffType<TinyBuff>()))
            {
                Projectile.timeLeft = 2;
            }
            #endregion
            // Teleport to player if distance is too big
            
            if (Main.myPlayer == player.whoAmI && distanceToIdlePosition > 2000f)
            {
                
                Projectile.position = idlepos;
                Projectile.velocity *= 0.1f;
                Projectile.netUpdate = true;
            }

            //Find target
            NPC nearestnpc = nearestNPC(distanceFromTarget);

            if (!foundTarget)
            {
                if(nearestnpc != null)
                {
                    foundTarget = true;
                    float between = Vector2.Distance(nearestnpc.Center, Projectile.Center);
                    bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, nearestnpc.position, nearestnpc.width, nearestnpc.height);
                    if (lineOfSight)
                    {
                        distanceFromTarget = between;
                        targetCenter = nearestnpc.Center;
                        foundTarget = true;

                    }
                }
                
               
            }
            
            
            float overlapVelocity = 0.04f;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                // Fix overlap with other minions
                Projectile other = Main.projectile[i];
                if (i != Projectile.whoAmI && other.active && other.owner == Projectile.owner && Math.Abs(Projectile.position.X - other.position.X) + Math.Abs(Projectile.position.Y - other.position.Y) < Projectile.width)
                {
                    if (Projectile.position.X < other.position.X) Projectile.velocity.X -= overlapVelocity;
                    else Projectile.velocity.X += overlapVelocity;

                    if (Projectile.position.Y < other.position.Y) Projectile.velocity.Y -= overlapVelocity;
                    else Projectile.velocity.Y += overlapVelocity;
                }
            }
            // movement for the summon

            float speed = 8f;

            float inertia = 40f;

            //if we found a target, we should fly towards that target and fire at it
            if (foundTarget)
            {
                if (distanceFromTarget > 40f)
                {
                    Vector2 end = targetCenter;
                    Vector2 Direction = end - start;
                    Direction.Normalize();
                    Direction *= speed;
                    Projectile.velocity = (Projectile.velocity * (inertia - 1) + Direction) / inertia;
                    Projectile.ai[0]++;
                    if (Projectile.ai[0] > 10f)
                    {
                        Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, Direction, ModContent.ProjectileType<TinyProj>(), 100, 6f, Owner: player.whoAmI);
                        Projectile.ai[0] = 0;
                    }
                }

            }
            else
            {
                if (distanceToIdlePosition > 600f)
                {
                    speed = 12f;
                    inertia = 60f;
                }
                else
                {
                    speed = 4f;
                    inertia = 80f;
                }
                if (distanceToIdlePosition > 20f)
                {
                    // The immediate range around the player (when it passively floats about)

                    // This is a simple movement formula using the two parameters and its desired direction to create a "homing" movement
                    vectorToIdlePosition.Normalize();
                    vectorToIdlePosition *= speed;
                    Projectile.velocity = (Projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
                }
                else if (Projectile.velocity == Vector2.Zero)
                {
                    // If there is a case where it's not moving at all, give it a little "poke"
                    Projectile.velocity.X = -0.15f;
                    Projectile.velocity.Y = -0.05f;
                }
            }

            //animation
            Projectile.rotation *= Projectile.velocity.X * 0.05f;


            float hue = 0f;

            hue += 0.01f;

            if (hue >= 1f) {hue -= 1f;}
            Color color = ColorFromHSV(hue, 1f, 1f);
            // Multiply the color by the desired intensity
            float intensity = 0.78f;
            Vector3 lightColor = color.ToVector3() * intensity;
            // Add the light with the calculated color
            Lighting.AddLight(Projectile.Center, lightColor);
            int framespeed = 7;
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= framespeed)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                }
            }
            
        }

        public override void SetDefaults()
        {
            // Only controls if it deals damage to enemies on contact (more on that later)
            Projectile.friendly = true;
            // Only determines the damage type
            Projectile.minion = true;
            // Amount of slots this minion occupies from the total minion slots available to the player (more on that later)
            Projectile.minionSlots = 1f;
            // Needed so the minion doesn't despawn on collision with enemies or tiles
            Projectile.penetrate = -1;
        }
    }
    public class TinyStaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.knockBack = 3f;
            Item.mana = 10;
            Item.width = 44;
            Item.height = 44;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.buyPrice(0, 30, 0, 0);
            Item.rare = ItemRarityID.Cyan;
            Item.UseSound = SoundID.Item44;

            

            Item.noMelee = true;
            Item.DamageType = DamageClass.Summon;
            Item.buffType = ModContent.BuffType<TinyBuff>();
            Item.shoot = ModContent.ProjectileType<TinySoldier>();
        }


        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.AddBuff(Item.buffType, 2, true); //apply minion buff
            position = Main.MouseWorld; //spawn at cursor
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe() //this takes a param in the '()' for how many gets created at a time, by default it is 1.
                .AddIngredient(ItemID.ChlorophyteBar, 20)
                .AddTile(TileID.MythrilAnvil) //addtile takes tile id for crafting (like if it takes a anvil or a work bench)
                .Register(); //register registers the item.
        }
    }
}
