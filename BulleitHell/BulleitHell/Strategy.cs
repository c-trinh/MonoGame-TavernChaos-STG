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
    abstract class Strategy
    {
        public abstract void Move(Enemy e);
        public abstract int MoveBullet(Bullet b);
    }

    class GruntMovement : Strategy
    {
        public override void Move(Enemy e)
        {
            if(e.ai_direction == 0 && e.ai_direction2 == 0)
            {
                e.ai_direction = 1;
                e.ai_direction2 = (float).3;
            }
            //Console.WriteLine("In gruntmovement strat Move()\n");
            if ((e.entPosition.Y >= 50 && e.entPosition.Y <= 90)
                    || e.entPosition.Y >= (Property.ScreenHeight - 350))
                e.ai_direction *= -1;
            if ((e.entPosition.X >= 50 && e.entPosition.X <= 90)
                || e.entPosition.X >= (Property.ScreenWidth - 100))
                e.ai_direction2 *= (float)-1;
            e.entPosition.Y += e.ENEMY_SPD * e.ai_direction;
            e.entPosition.X += e.ENEMY_SPD * e.ai_direction2;

            e.hitbox.Width = 100;
            e.hitbox.Height = 100;

            e.hitbox.X = (int)e.entPosition.X;
            e.hitbox.Y = (int)e.entPosition.Y; //may need to do this.height/2

            if (e.FrameDelay > 0)
                e.FrameDelay--;

        }
        public override int MoveBullet(Bullet b)
        {
            throw new NotImplementedException();
        }
    }

    class GruntMovement2 : Strategy
    {
        public override void Move(Enemy e)
        {
            if (e.ai_direction == 0 && e.ai_direction2 == 0)
            {
                e.ai_direction = 1;
                e.ai_direction2 = (float).3;
            }

            //Console.WriteLine("In gruntmovement strat Move()\n");
            if (e.entPosition.Y > (Property.ScreenHeight - 400))
            {
                e.ai_direction *= 0;
                e.entPosition.Y = Property.ScreenHeight - 400;
            }
            if ((e.entPosition.X <= 200 || e.entPosition.X >= Property.ScreenWidth - 200) && e.ai_direction == 0)
            {
                e.ai_direction = -1;
            }
            if ((e.entPosition.X <= 10 || e.entPosition.X >= Property.ScreenWidth - 30) && e.ai_direction == -1)
            {
                e.ai_direction = 1;
                e.ai_direction2 *= (float)-1;
            }
            e.entPosition.Y += e.ENEMY_SPD * e.ai_direction;
            e.entPosition.X += e.ENEMY_SPD * e.ai_direction2 * 2;

            e.hitbox.Width = 100;
            e.hitbox.Height = 100;

            e.hitbox.X = (int)e.entPosition.X;
            e.hitbox.Y = (int)e.entPosition.Y; //may need to do this.height/2

            if (e.FrameDelay > 0)
                e.FrameDelay--;

        }
        public override int MoveBullet(Bullet b)
        {
            throw new NotImplementedException();
        }
    }

    class GruntMovement3 : Strategy
    {
        public override void Move(Enemy e)
        {
            if (e.ai_direction == 0 && e.ai_direction2 == 0)
            {
                e.ai_direction = -1;
                e.ai_direction2 = (float).3;
            }
            //Console.WriteLine("In gruntmovement strat Move()\n");
            if (e.entPosition.Y < 100)
            {
                e.ai_direction *= 0;
                e.entPosition.Y = 100;
            }
            if ((e.entPosition.X <= 200 || e.entPosition.X >= Property.ScreenWidth - 200) && e.ai_direction == 0)
            {
                e.ai_direction = 1;
            }
            if ((e.entPosition.X <= 10 || e.entPosition.X >= Property.ScreenWidth - 30) && e.ai_direction == 1)
            {
                e.ai_direction = -1;
                e.ai_direction2 *= (float)-1;
            }
            e.entPosition.Y += e.ENEMY_SPD * e.ai_direction;
            e.entPosition.X += e.ENEMY_SPD * e.ai_direction2 * 2;

            e.hitbox.Width = 100;
            e.hitbox.Height = 100;

            e.hitbox.X = (int)e.entPosition.X;
            e.hitbox.Y = (int)e.entPosition.Y; //may need to do this.height/2

            if (e.FrameDelay > 0)
                e.FrameDelay--;

        }
        public override int MoveBullet(Bullet b)
        {
            throw new NotImplementedException();
        }
    }

    class MidbossMovement : Strategy
    {
        public override void Move(Enemy e)
        {
            if (e.ai_direction == 0 && e.ai_direction2 == 0)
            {
                e.ai_direction2 = (float)1;
            }
            //Console.WriteLine("In midboss movement Move()\n");
            if (e.entPosition.X < 50)
            {
                e.entPosition.X = 50;
                e.ai_direction2 = 0;
                if (e.entPosition.Y < Property.ScreenHeight / 2)
                    e.ai_direction = 1;
                else
                    e.ai_direction = -1;
            }
            else if (e.entPosition.X > Property.ScreenWidth - 50)
            {
                e.entPosition.X = Property.ScreenWidth - 50;
                e.ai_direction2 = 0;
                if (e.entPosition.Y < Property.ScreenHeight / 2)
                    e.ai_direction = 1;
                else
                    e.ai_direction = -1;
            }
            if (e.entPosition.Y < 50)
            {
                e.entPosition.Y = 50;
                e.ai_direction = 0;
                if (e.entPosition.X < Property.ScreenWidth / 2)
                    e.ai_direction2 = 1;
                else
                    e.ai_direction2 = -1;
            }
            else if (e.entPosition.Y > Property.ScreenHeight - 400)
            {
                e.entPosition.Y = Property.ScreenHeight - 400;
                e.ai_direction = 0;
                if (e.entPosition.X < Property.ScreenWidth / 2)
                    e.ai_direction2 = 1;
                else
                    e.ai_direction2 = -1;
            }

            e.entPosition.X += e.ENEMY_SPD * e.ai_direction2;
            e.entPosition.Y += e.ENEMY_SPD * e.ai_direction;

            e.hitbox.Width = (int)Math.Floor((double)e.entTexture.Width * e.size_scale); //CHANGE DIMENSIONS
            e.hitbox.Height = (int)Math.Floor((double)e.entTexture.Height * e.size_scale);

            e.hitbox.X = (int)e.entPosition.X;
            e.hitbox.Y = (int)e.entPosition.Y; //may need to do this.height/2

            if (e.FrameDelay > 0)
                e.FrameDelay--;
        }

        public override int MoveBullet(Bullet b)
        {
            throw new NotImplementedException();
        }
    }
    class FinalbossMovement : Strategy
    {
        public override void Move(Enemy e)
        {
            if (e.ai_direction == 0 && e.ai_direction2 == 0)
            {
                e.ai_direction2 = (float)1;
            }
            //Console.WriteLine("In midboss movement Move()\n");
            if (e.entPosition.X < 50)
            {
                e.entPosition.X = 50;
                e.ai_direction2 = 0;
                if (e.entPosition.Y < Property.ScreenHeight / 2)
                    e.ai_direction = 1;
                else
                    e.ai_direction = -1;
            }
            else if (e.entPosition.X > Property.ScreenWidth - 50)
            {
                e.entPosition.X = Property.ScreenWidth - 50;
                e.ai_direction2 = 0;
                if (e.entPosition.Y < Property.ScreenHeight / 2)
                    e.ai_direction = 1;
                else
                    e.ai_direction = -1;
            }
            if (e.entPosition.Y < 50)
            {
                e.entPosition.Y = 50;
                e.ai_direction = 0;
                if (e.entPosition.X < Property.ScreenWidth / 2)
                    e.ai_direction2 = 1;
                else
                    e.ai_direction2 = -1;
            }
            else if (e.entPosition.Y > Property.ScreenHeight - 400)
            {
                e.entPosition.Y = Property.ScreenHeight - 400;
                e.ai_direction = 0;
                if (e.entPosition.X < Property.ScreenWidth / 2)
                    e.ai_direction2 = 1;
                else
                    e.ai_direction2 = -1;
            }

            e.entPosition.X += e.ENEMY_SPD * e.ai_direction2;
            e.entPosition.Y += e.ENEMY_SPD * e.ai_direction;

            e.hitbox.Width = (int)Math.Floor((double)e.entTexture.Width * e.size_scale); //CHANGE DIMENSIONS
            e.hitbox.Height = (int)Math.Floor((double)e.entTexture.Height * e.size_scale);

            e.hitbox.X = (int)e.entPosition.X;
            e.hitbox.Y = (int)e.entPosition.Y; //may need to do this.height/2

            if (e.FrameDelay > 0)
                e.FrameDelay--;
        }

        public override int MoveBullet(Bullet b)
        {
            throw new NotImplementedException();
        }
    }

    //context


    class PlayerBulletMovement : Strategy
    {
        public override void Move(Enemy e)
        {
            throw new NotImplementedException();
        }

        public int OOBCheck(Bullet b)
        {
            //Console.WriteLine("in oob check in strat\n");
            if (b.entPosition.X + b.entTexture.Bounds.Width <= 0 || b.entPosition.X >= Property.ScreenWidth || b.entPosition.Y >= Property.ScreenHeight || b.entPosition.Y + b.entTexture.Bounds.Height <= Property.ScreenHeight)
            {
                b.active = 0;
            }
            return b.active;
        }
        public override int MoveBullet(Bullet b)
        {
            //Console.WriteLine("In movebullet strat\n");
            b.entPosition.Y += -1 * b.entSpeed;
            //b.hitbox.Width = (int)Math.Floor((double)b.entTexture.Width * b.size_scale); //CHANGE DIMENSIONS
            //b.hitbox.Height = (int)Math.Floor((double)b.entTexture.Height * b.size_scale);

            b.hitbox.X = (int)b.entPosition.X;
            b.hitbox.Y = (int)b.entPosition.Y; //may need to do this.height/2

            if (b.is_spin)
                b.angle -= 0.04f;

            return OOBCheck(b);
        }
    }

    class BulletMovement : Strategy
    {
        public override void Move(Enemy e)
        {
            throw new NotImplementedException();
        }

        public int OOBCheck(Bullet b)
        {
            //Console.WriteLine("in oob check in strat\n");
            if (b.entPosition.X + b.entTexture.Bounds.Width <= 0 || b.entPosition.X >= Property.ScreenWidth || b.entPosition.Y >= Property.ScreenHeight || b.entPosition.Y + b.entTexture.Bounds.Height <= Property.ScreenHeight)
            {
                b.active = 0;
            }
            return b.active;
        }
        public override int MoveBullet(Bullet b)
        {
            //Console.WriteLine("In movebullet strat\n");
            b.entPosition.Y += 1 * b.entSpeed;
            //b.hitbox.Width = (int)Math.Floor((double)b.entTexture.Width * b.size_scale); //CHANGE DIMENSIONS
            //b.hitbox.Height = (int)Math.Floor((double)b.entTexture.Height * b.size_scale);

            b.hitbox.X = (int)b.entPosition.X;
            b.hitbox.Y = (int)b.entPosition.Y; //may need to do this.height/2

            if (b.is_spin)
                b.angle -= 0.04f;

            return OOBCheck(b);
        }
    }

    class BulletMovement2 : Strategy
    {
        public override void Move(Enemy e)
        {
            throw new NotImplementedException();
        }

        public int OOBCheck(Bullet b)
        {
            //Console.WriteLine("in oob check in strat\n");
            if (b.entPosition.X + b.entTexture.Bounds.Width <= 0 || b.entPosition.X >= Property.ScreenWidth || b.entPosition.Y >= Property.ScreenHeight || b.entPosition.Y + b.entTexture.Bounds.Height <= Property.ScreenHeight)
            {
                b.active = 0;
            }
            return b.active;
        }
        public override int MoveBullet(Bullet b)
        {
            if (b.direction.X == 0 && b.direction.Y == 0)
            {
                Vector2 pos;
                Random rand = new Random();
                int dir = rand.Next(3);
                switch(dir)
                {
                    case 0:
                        pos = new Vector2(0, 1);
                        break;
                    case 1:
                        pos = new Vector2((float)0.5, 1);
                        break;
                    case 2:
                        pos = new Vector2((float)-0.5, 1);
                        break;
                    default:
                        pos = new Vector2(0, 1);
                        break;

                }
                b.direction = pos;
            }
            //Console.WriteLine("In movebullet strat\n");
            b.entPosition.X += b.direction.X * b.entSpeed;
            b.entPosition.Y += b.direction.Y * b.entSpeed;
            //b.hitbox.Width = (int)Math.Floor((double)b.entTexture.Width * b.size_scale); //CHANGE DIMENSIONS
            //b.hitbox.Height = (int)Math.Floor((double)b.entTexture.Height * b.size_scale);

            b.hitbox.X = (int)b.entPosition.X;
            b.hitbox.Y = (int)b.entPosition.Y; //may need to do this.height/2

            if (b.is_spin)
                b.angle -= 0.04f;

            return OOBCheck(b);
        }
    }

    class BulletMovement3 : Strategy
    {
        public override void Move(Enemy e)
        {
            throw new NotImplementedException();
        }

        public int OOBCheck(Bullet b)
        {
            //Console.WriteLine("in oob check in strat\n");
            if (b.entPosition.X + b.entTexture.Bounds.Width <= 0 || b.entPosition.X >= Property.ScreenWidth || b.entPosition.Y >= Property.ScreenHeight || b.entPosition.Y + b.entTexture.Bounds.Height <= Property.ScreenHeight)
            {
                b.active = 0;
            }
            return b.active;
        }
        public override int MoveBullet(Bullet b)
        {
            //Console.WriteLine("In movebullet strat\n");
            if (b.direction.X == 0)
            {
                b.direction.X = (float).3;
                b.direction.Y = (float)1;

            }

            if (b.entPosition.X > b.spawnLocation.X + 20)
            {
                b.direction.X *= -1;
            }
            if (b.entPosition.X < b.spawnLocation.X - 20)
            {
                b.direction.X *= -1;
            }
            b.entPosition.X += b.direction.X * b.entSpeed;
            b.entPosition.Y += b.direction.Y * b.entSpeed;
            //b.hitbox.Width = (int)Math.Floor((double)b.entTexture.Width * b.size_scale); //CHANGE DIMENSIONS
            //b.hitbox.Height = (int)Math.Floor((double)b.entTexture.Height * b.size_scale);

            b.hitbox.X = (int)b.entPosition.X;
            b.hitbox.Y = (int)b.entPosition.Y; //may need to do this.height/2

            if (b.is_spin)
                b.angle -= 0.04f;

            return OOBCheck(b);
        }
    }

    class MovedEnemy
    {
        private Strategy movementStrat;

        public void SetMovement(Strategy movement)
        {
            this.movementStrat = movement;
        }

        public void Move(Enemy e)
        {
            movementStrat.Move(e);
        }
        public void MoveBullet(Bullet b)
        {
            movementStrat.MoveBullet(b);
        }



    }
}
