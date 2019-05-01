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

    /*
        powerup will be entity based, and build heavily off of the enemy class.
        spawn based on some sort of random chance after killing an enemy
        activation to give powerup will be based on a collision detection between player and powerup

        maybe have a generic function to receive the powerup as a string based param?
            
     */
    public class PowerUp : Entity
    {
        protected readonly int SPD = 5; //just set one constant speed? at least for now
        private Property.PowerupType Type;
        public PowerUp(Vector2 pos, float size_scale, Property.PowerupType newType, Texture2D sprite)
        {
            entPosition = pos;
            this.size_scale = size_scale;
            active = 1;
            entTexture = sprite;
            Console.WriteLine("Pos x: {0}", pos.X);
            Console.WriteLine("Pos y: {0}", pos.Y);

            Type = newType; //what type of powerup are we working with
        }

        public void Update()
        {
            // hitbox.Y += SPD; // powerups will likely only travel down so just go only that way.

            hitbox.Width = (int)Math.Floor((double)entTexture.Width * size_scale); //CHANGE DIMENSIONS
            hitbox.Height = (int)Math.Floor((double)entTexture.Height * size_scale);

            hitbox.X = (int)entPosition.X;
            hitbox.Y = (int)entPosition.Y; //may need to do this.height/2
        }

        public Property.PowerupType GivePower()
        {
            //give plus 1 health, plus 1 dmg?
            //do this based on a string
            active = 0;
            return Type;
        
        }
    }
}
