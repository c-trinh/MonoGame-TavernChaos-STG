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
    public class Enemy : Entity
    {
        public readonly int ENEMY_SPD = 5;
        public int ai_direction = 0;
        public float ai_direction2 = 0;
        private BulletFactory gun;
        public int FrameDelay;
        public int ai_direction_y = 0;
        Random rand = new Random();
        public bool isFinBoss;
        public int movementStrat = 0;
        public int bulletStrat = 0;
        

        //temporary to create changing bullet behavior
        //int bulletState = 0; //this will cycle 0,1,2,0,1,2....


        public virtual void Update()
        {
            //enemy movement
            if ((entPosition.X >= 50 && entPosition.X <= 100)
                || entPosition.X >= (Property.ScreenWidth - 100))
                ai_direction *= -1;

            if ((entPosition.Y >= 50 && entPosition.Y <= 90)
                    || entPosition.Y >= (Property.ScreenHeight - 350))
                ai_direction2 *= (float)-1;

            entPosition.X += ENEMY_SPD * ai_direction;
            entPosition.Y += ENEMY_SPD * ai_direction2;

            hitbox.Width = (int)Math.Floor((double)entTexture.Width * size_scale); //CHANGE DIMENSIONS
            hitbox.Height = (int)Math.Floor((double)entTexture.Height * size_scale);

            hitbox.X = (int)entPosition.X;
            hitbox.Y = (int)entPosition.Y; //may need to do this.height/2

            if (FrameDelay > 0)
                FrameDelay--;

            //Console.WriteLine("Ent Position" + entPosition);
        }

        //constructor
        public Enemy(Vector2 pos, float size_scale, int movementStrategy, int bulletStrategy)
        {
            this.bulletStrat = bulletStrategy;
            this.movementStrat = movementStrategy;
            entPosition = pos;
            this.size_scale = size_scale;
            gun = new BulletFactory();
            ENEMY_SPD = rand.Next(3, 6);
            int rand_dir = rand.Next(0, 2);
            active = 1;
            /*
            switch (rand_dir)
            {
                case 0:
                    ai_direction = 1;
                    break;
                case 1:
                    ai_direction = -1;
                    break;
            }
            */
            //Console.WriteLine("Pos x: {0}", pos.X);
            //Console.WriteLine("Pos y: {0}", pos.Y);

        }

        /*      -----> factories are now invoked in the GameManager
        public Bullet Shoot()
        {

            return gun.CreateBullet(Property.BulletType.Red, this.entPosition, new Vector2(0, 1), (float)3.15);
        }
        */

        public void TakeDamage()
        {
            if(FrameDelay==0)
            {
                if (health > 0)
                {
                    health--;
                    FrameDelay = 15;
                    //Console.WriteLine("enemy took dmg");
                }

                if (health < 1)
                {
                    //function to call die?
                    active = 0;
                }
            }

        }

        public virtual Bullet CreateBullet(Property.BulletType t, float angle, BulletFactory factory)
        {
            Vector2 direction = new Vector2(0, 0);


            return factory.CreateBullet(t, this.entPosition, direction, (float)3.15, bulletStrat);
        }

    }
}
