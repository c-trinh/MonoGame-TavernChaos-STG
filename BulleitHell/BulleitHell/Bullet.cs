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
    public class Bullet : Entity
    {
        private Vector2 direction; //Basic direction modifier meant to be between -1 and 1 to represent direction in all axis. 
        private int active;                           //the value is never greater than 1 to leave the speed up to the speed variable.

        public Bullet(Texture2D sprite, Vector2 position)
        {
            int active = 1;
            entTexture = sprite;
            this.entPosition = position;
            Vector2 downDir = new Vector2(0, 1);
            direction = downDir;
            entSpeed = 10;
        }
        
        //Update returns the result of an out of bounds check. If the object is out of bounds, 0 is returned and the object can be removed from its containing list
        public int Update()
        {
            
            entPosition.X += direction.X * entSpeed;
            entPosition.Y += direction.Y * entSpeed;

            return OOBCheck();
        }

        public int OOBCheck()
        {
            if(entPosition.X + this.entTexture.Bounds.Width <= 0 || entPosition.X >= GameProperties.ScreenWidth || entPosition.Y >= GameProperties.ScreenHeight || entPosition.Y + this.entTexture.Bounds.Height <= GameProperties.ScreenHeight)
            {
                active = 0;
            }

            return active;
        }



    }
}
