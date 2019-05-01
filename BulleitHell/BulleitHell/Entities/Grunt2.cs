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
    class Grunt2:Enemy
    {
        public Grunt2(Vector2 pos, int movementStrategy, int bulletStrategy) : base(pos, (float)1.15, movementStrategy, bulletStrategy)
        {
            Console.WriteLine("In G2 Constructor");
            health = 2;//placeholder, update later
            entTexture = Property.spriteDict["grunt_2"];
            isFinBoss = false;
        }

        public override void Update()
        {
            Console.WriteLine("In grunt2 update()\n");
            if ((entPosition.Y >= 50 && entPosition.Y <= 90)
                    || entPosition.Y >= (Property.ScreenHeight - 350))
                ai_direction *= -1;
            if ((entPosition.X >= 50 && entPosition.X <= 90)
                || entPosition.X >= (Property.ScreenWidth - 100))
                ai_direction2 *= (float)-1;
            entPosition.Y += ENEMY_SPD * ai_direction;
            entPosition.X += ENEMY_SPD * ai_direction2;

            hitbox.Width = 100;
            hitbox.Height = 100;

            hitbox.X = (int)entPosition.X;
            hitbox.Y = (int)entPosition.Y; //may need to do this.height/2

            if (FrameDelay > 0)
                FrameDelay--;

            //Console.WriteLine("Ent Position" + entPosition);
        }
    }
}
