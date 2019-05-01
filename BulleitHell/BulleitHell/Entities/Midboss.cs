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
    class MidBoss : Enemy
    {
       public MidBoss(Vector2 pos, float size_scale, int movementStrategy, int bulletStrategy) : base(pos, size_scale, movementStrategy, bulletStrategy)
       {
            health = 5;//placeholder
            entTexture = Property.spriteDict["mid_boss"];
            isFinBoss = false;
       }

        public override void Update()
        {
            Console.WriteLine("In mid boss update()\n");
            if (entPosition.X < 50)
            {
                entPosition.X = 50;
                ai_direction = 0;
                if (entPosition.Y < Property.ScreenHeight / 2)
                    ai_direction_y = 1;
                else
                    ai_direction_y = -1;
            }
            else if (entPosition.X > Property.ScreenWidth - 50)
            {
                entPosition.X = Property.ScreenWidth - 50;
                ai_direction = 0;
                if (entPosition.Y < Property.ScreenHeight / 2)
                    ai_direction_y = 1;
                else
                    ai_direction_y = -1;
            }
            if (entPosition.Y < 50)
            {
                entPosition.Y = 50;
                ai_direction_y = 0;
                if (entPosition.X < Property.ScreenWidth / 2)
                    ai_direction = 1;
                else
                    ai_direction = -1;
            }
            else if (entPosition.Y > Property.ScreenHeight - 400)
            {
                entPosition.Y = Property.ScreenHeight - 400;
                ai_direction_y = 0;
                if (entPosition.X < Property.ScreenWidth / 2)
                    ai_direction = 1;
                else
                    ai_direction = -1;
            }

            entPosition.X += ENEMY_SPD * ai_direction;
            entPosition.Y += ENEMY_SPD * ai_direction_y;

            hitbox.Width = (int)Math.Floor((double)entTexture.Width * size_scale); //CHANGE DIMENSIONS
            hitbox.Height = (int)Math.Floor((double)entTexture.Height * size_scale);

            hitbox.X = (int)entPosition.X;
            hitbox.Y = (int)entPosition.Y; //may need to do this.height/2

            if (FrameDelay > 0)
                FrameDelay--;
        }

        public override Bullet CreateBullet(Property.BulletType t, float angle, BulletFactory factory)  // Bosses must override CreatBullet() to implement the decorator
        {
            return base.CreateBullet(t, angle, factory);
        }
    }
}
