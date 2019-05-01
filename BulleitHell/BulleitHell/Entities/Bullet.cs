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
        public Vector2 direction; //Basic direction modifier meant to be between -1 and 1 to represent direction in all axis.
        //private int active;                           //the value is never greater than 1 to leave the speed up to the speed variable.
        public int size;
        //public Rectangle hitbox;
        public bool is_spin;

        public int movementPattern = 0;

        public Vector2 spawnLocation;

        public Bullet(Texture2D sprite, Vector2 position, Vector2 direction, float angle, bool is_spin, int movementPattern)
        {
            spawnLocation = position;
            this.movementPattern = movementPattern;
            active = 1;
            entTexture = sprite;
            this.entPosition = position;
            this.direction = direction;
            this.entSpeed = 6;
            this.angle = angle;
            this.is_spin = is_spin;
            size = 40;
            this.origin = new Vector2(size / 2f, size / 2f);
            hitbox.Width = 38;
            hitbox.Height = 38;
            hitbox.X = (int)position.X;
            hitbox.Y = (int)position.Y;
        }

        //Update returns the result of an out of bounds check. If the object is out of bounds, 0 is returned and the object can be removed from its containing list
        public int Update()
        {

            entPosition.X += direction.X * entSpeed;
            entPosition.Y += direction.Y * entSpeed;

            hitbox.X = (int)entPosition.X;
            hitbox.Y = (int)entPosition.Y;

            if (is_spin)
                this.angle -= 0.04f;


            return OOBCheck();
        }

        public int OOBCheck()
        {
            if(entPosition.X + this.entTexture.Bounds.Width <= 0 || entPosition.X >= Property.ScreenWidth || entPosition.Y >= Property.ScreenHeight || entPosition.Y + this.entTexture.Bounds.Height <= Property.ScreenHeight)
            {
                active = 0;
            }
            return active;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //doesnt inherently need to be a color here
            //spriteBatch.Draw(entTexture, entPosition, Color.White); //Change to take Angle

            spriteBatch.Draw(entTexture, new Rectangle((int)entPosition.X, (int)entPosition.Y, size, size), null, Color.White, angle, origin, SpriteEffects.None, 0f);
        }

        public void CheckBulletCollisions()
        {
            // Only called from a special bullet
            // This will update the speed of all colliding minor bullets

        }
    }
}
