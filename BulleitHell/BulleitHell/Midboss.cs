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
    class MidBoss:Enemy
    {
       public MidBoss(Texture2D texture, Vector2 pos) : base(texture, pos)
       {
       Console.WriteLine("In MidBoss Constructor");
            entTexture = texture;

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //doesnt inherently need to be a color here
            spriteBatch.Draw(entTexture, entPosition, Color.White);
        }
    }
}
