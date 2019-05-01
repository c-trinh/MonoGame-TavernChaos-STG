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
        This is the abstract entity class. 
        It encapsulates the following variables:
            a texture, postion, speed variables
         The following base functions are added:
            Load - initializing the texture 
            draw - for making easy draw calls within the main loop
        
        Functions that are not implemented yet due to design ambiguity
            Update: updates are manual in AI and bullet entities, but require the keyboard input for the player class
                    There wasnt a convienent way to allow an override that adds additional parameters, so i left it to be manually implemented on inherited classes
    */
    public abstract class Entity
    {
        public Texture2D entTexture;
        public Vector2 entPosition;
        protected Vector2 origin;
        public int entSpeed = 8; //this is temporary and will get changed
        protected int health; //this will get populated per class that is using it
        public float angle = 0; //this will get populated per class that is using it
        public Rectangle hitbox;
        public float size_scale = 1;
        public int active;

        

        public void Load(Texture2D texture)
        {
            entTexture = texture;
        }

        public void RandomSpawn() {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            origin = new Vector2(entTexture.Width / 2, entTexture.Height / 2);
            spriteBatch.Draw(entTexture, new Rectangle((int)entPosition.X, (int)entPosition.Y, (int)(entTexture.Width * size_scale), (int)(entTexture.Height * size_scale)), null, Color.White, angle, origin, SpriteEffects.None, 0f);
        }

    }
}
