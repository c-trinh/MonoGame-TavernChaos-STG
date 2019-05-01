using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BulleitHell.Entities;

namespace BulleitHell
{
    class FinalBoss:Enemy
    {
        protected int ai_direction_y = 0;

        public FinalBoss(Vector2 pos, float size_scale, int movementStrategy, int bulletStrategy) : base(pos, size_scale, movementStrategy, bulletStrategy)
        {
            Console.WriteLine("In FinalBoss Constructor");
            entTexture = Property.spriteDict["final_boss"];
            health = 5;
            isFinBoss = true;
        }

        public override void Update()
        {
            Console.WriteLine("In final boss update()\n");
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

            hitbox.X = (int)entPosition.X;
            hitbox.Y = (int)entPosition.Y; //may need to do this.height/2

            if (FrameDelay > 0)
                FrameDelay--;
        }

        public override Bullet CreateBullet(Property.BulletType t, float angle, BulletFactory factory)  // Bosses must override CreatBullet() to implement the decorator
        {
            return base.CreateBullet(t, angle, factory);
        }

        public Bullet CreateSpecialBullet(BulletFactory bFact)
        {
            // Pass in the proper factory and get the bullet back
            ExternalBulletDecorator NewSpecialBulletCreator = new ExternalBulletDecorator(bFact);
            // This constructor handles the creation of the special bullet and save it to the DecoratedBullet property

            return NewSpecialBulletCreator.DecoratedBullet;
        }

        public void FireSpecialAttack(BulletFactory factory)
        {
            Vector2 direction = new Vector2(0, 1);
            Vector2 currentPos = this.entPosition;
            currentPos.X -= 140;
            currentPos.Y += 64;

            // this function calls the CreateBullet pattern 8 times passing the modified spawn position each time then calls Create special bullet

            Property.enemyBullets.Add(factory.CreateBullet(Property.BulletType.Yellow, currentPos, direction, 0, this.bulletStrat));
            currentPos.X += 40;
            Property.enemyBullets.Add(factory.CreateBullet(Property.BulletType.Yellow, currentPos, direction, 0, this.bulletStrat));
            currentPos.X += 40;
            Property.enemyBullets.Add(factory.CreateBullet(Property.BulletType.Yellow, currentPos, direction, 0, this.bulletStrat));
            currentPos.X += 40;
            Property.enemyBullets.Add(factory.CreateBullet(Property.BulletType.Yellow, currentPos, direction, 0, this.bulletStrat));
            currentPos.X += 40;
            Property.enemyBullets.Add(factory.CreateBullet(Property.BulletType.Yellow, currentPos, direction, 0, this.bulletStrat));
            currentPos.X += 40;
            Property.enemyBullets.Add(factory.CreateBullet(Property.BulletType.Yellow, currentPos, direction, 0, this.bulletStrat));
            currentPos.X += 40;
            Property.enemyBullets.Add(factory.CreateBullet(Property.BulletType.Yellow, currentPos, direction, 0, this.bulletStrat));
            currentPos.X += 40;
            Property.enemyBullets.Add(factory.CreateBullet(Property.BulletType.Yellow, currentPos, direction, 0, this.bulletStrat));

            currentPos.X -= 170;
            currentPos.Y -= 96;
            Property.enemyBullets.Add(factory.CreateBullet(Property.BulletType.Special, currentPos, direction, 0, this.bulletStrat));
        }
    }
}
