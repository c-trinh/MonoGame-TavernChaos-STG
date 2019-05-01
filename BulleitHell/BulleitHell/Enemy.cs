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
        private readonly int W_WIDTH = 1280;
        private readonly int W_HEIGHT = 800;
        private readonly int ENEMY_SPD = 5;
        private int ai_direction = 1;
        private BulletFactory gun;
        Random rand = new Random();

        public void Update()
        {
           
            //enemy movement
            //Enemy Basic AI taken from game.cs
            if ((entPosition.X >= 50 && entPosition.X <= 100)
                || (entPosition.X >= (this.W_WIDTH - 256 - 25) && entPosition.X >= (this.W_WIDTH - 256 - 25)))
                ai_direction *= -1;
            entPosition.X += ENEMY_SPD * ai_direction;
            entPosition.Y += ENEMY_SPD * (float) 0.20;
            //Console.WriteLine("Ent Position" + entPosition);
        }

       //constructor
        public Enemy(Texture2D texture, Vector2 pos)
        {
            entTexture = texture;
            entPosition = pos;
            gun = new BulletFactory();
            ENEMY_SPD = rand.Next(1, 7);
            int rand_dir = rand.Next(0, 2);
            switch (rand_dir)
            {
                case 0:
                    ai_direction = 1;
                    break;
                case 1:
                    ai_direction = -1;
                    break;
            }
        }

        public Bullet Shoot()
        {
            return gun.CreateBullet(Game.BulletType.Yellow, this.entPosition);
        }
    }
}
