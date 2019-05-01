using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BulleitHell
{
    public class Player : Entity
    {
        int lives;
        bool dmgStatus = false;
        private BulletFactory gun;
        int FrameDelay;
        public bool dead { private set; get; }
        public bool invuln { private set; get; }


        //player controls to update positon
        public void Update(KeyboardState keyPress)
        {
            invuln = false;
            /// Player Speed
            if (keyPress.IsKeyDown(Keys.LeftShift))
                entSpeed = 4;
            if (!keyPress.IsKeyDown(Keys.LeftShift))
                entSpeed = 8;
            
            /// Player Movement
            if (keyPress.IsKeyDown(Keys.Right))
                entPosition.X += entSpeed;
            if (keyPress.IsKeyDown(Keys.Left))
                entPosition.X -= entSpeed;
            if (keyPress.IsKeyDown(Keys.Up))
                entPosition.Y -= entSpeed;
            if (keyPress.IsKeyDown(Keys.Down))
                entPosition.Y += entSpeed;
            if (keyPress.IsKeyDown(Keys.G))
                invuln = true;

            //this can get removed, is just for debugging
            if (keyPress.IsKeyDown(Keys.P))
                dmgStatus = true;
            if(keyPress.IsKeyUp(Keys.P) && dmgStatus)
            {
                TakeDamage();
                //entPosition= new Vector2((Property.ScreenWidth - 128) / 2, Property.ScreenHeight - 128); //reset position to center when damaged

                dmgStatus = false;
            }

            CheckCollisions();
            //TODO CHANGE DIMENSIONS
            hitbox.Width = ((int)Math.Floor((double)entTexture.Width * size_scale)) - 100; //CHANGE DIMENSIONS
            hitbox.Height = ((int)Math.Floor((double)entTexture.Height * size_scale)) - 100;

            hitbox.X = (int)entPosition.X;
            hitbox.Y = (int)entPosition.Y; //may need to do this.height/2

            if (FrameDelay > 0)
                FrameDelay--;

            if (health >= 1)
            {
                entTexture = Property.spriteDict["player"];
            }
            if (health < 1)
            {
                entTexture = Property.spriteDict["player_wasted"];
            }
        }

        public void CheckCollisions()
        {
            if (hitbox.Intersects(hitbox))
            {
                
                
            }
        }

        //basic constructor, in which we only assign initial position
        //this can obviously be changed as needed
        public Player(Vector2 pos)
        {
            gun = new BulletFactory();
            entPosition = pos;
            health = 1;
            lives = 20;
            FrameDelay = 0;
            dead = false;
        }

        public Bullet Shoot()
        {
            //this.entPosition
            return gun.CreateBullet(Property.BulletType.Green, new Vector2(this.entPosition.X,(this.entPosition.Y-100)), new Vector2(0, -1), 0, 0);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //mod function creates a fast blinking effect to notify of damage
            if(FrameDelay==0 || FrameDelay % 5 == 0)
                base.Draw(spriteBatch);
        }

        public bool TakeDamage()
        {
            if (invuln)
            {
                return false;
            }
            if(FrameDelay == 0 )
            {
                if (health >= 0)
                {   //set a 1 second delay for next damage
                    health--;
                    FrameDelay = 60;
                    Console.WriteLine("took damage, {0} frame delay", FrameDelay);
                }

                if(health < 0 )
                {
                    //set a 2 second delay after death via frame delay (60 frames a second)
                    FrameDelay = 120;
                    Console.WriteLine("died, {0} frame delay", FrameDelay);

                    //function to call die?
                    // lives -- in the case of the player?s
                    lives--;
                    if (lives < 0)
                    {
                        Die();
                    }
                    health = 1; //reset player health
                    entPosition= new Vector2((Property.ScreenWidth - 128) / 2, Property.ScreenHeight - 128); //reset position to center when damaged

                }

                return true;
            }
            return false;
        }
            
        public void RecievePowerup(Property.PowerupType type)
        {
            //in here based on the string the player can receive various powerup types
            switch(type)
            {
                case Property.PowerupType.OneUp:
                    Property.SoundEffects["munch"].Play();
                    lives++;
                    break;
                case Property.PowerupType.Health:
                    health++;
                    Property.SoundEffects["vomit"].Play();
                    break;
                case Property.PowerupType.Bomb:
                    //add a function here to clear all enemies
                    //or deal all visual enemies 1 dmg?

                    foreach (var enemy in Property.enemies)
                    {
                        Property.inactiveEnemies.Add(enemy);    // add dead enemy to remove list
                        Property.SoundEffects["break"].Play();
                    }

                    Property.isBomb = true;
                    Property.SoundEffects["wave"].Play();
                    Property.SCORE += 5;
                    break;
            }
        }

        public string GetLives()
        {
            if (lives > -1)
                return lives.ToString();
            else
                return "DEAD";
        }

        public string GetHealth()
        {
            if (health <= 0)
                return "WASTED";
            return health.ToString();

        }

        public void Die()
        {
            dead = true;
            return;
        }
    }
}
