using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BulleitHell;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace BulleitHell
{

    public class EnemyFactory
    {

        Random rand = new Random();

        //the enemies we are going to have are going to be Grunt1 (1) Grunt2 (2) MidBoss (3) And final boss (4)

        public Vector2 GeneratePosition()
        {
            int x = rand.Next(400, 1000) - 128, y = rand.Next(0, GameProperties.ScreenHeight / 2);
            Debug.WriteLine("Position X: " + x.ToString());
            Debug.WriteLine("Position Y: " + y.ToString());
            Vector2 rand_position = new Vector2(x, y);
            return rand_position;
        }

        public Enemy CreateEnemy(Game.EnemyType enemy_type)
        {
            Texture2D enemy_texture;
            switch (enemy_type)
            {
                case Game.EnemyType.Grunt1:
                    Console.WriteLine("CREATED ENEMY:\tGREEN");

                    enemy_texture = Game.spriteDict["enemyGreen"];
                    return new Grunt1(enemy_texture, GeneratePosition());
                case Game.EnemyType.Grunt2:
                    Console.WriteLine("CREATED ENEMY:\tBLUE");

                    enemy_texture = Game.spriteDict["enemyBlue"];
                    return new Grunt2(enemy_texture, GeneratePosition());
                case Game.EnemyType.MidBoss:
                    Console.WriteLine("CREATED ENEMY:\tYELLOW");

                    enemy_texture = Game.spriteDict["enemyYellow"];
                    return new MidBoss(enemy_texture, GeneratePosition());
                case Game.EnemyType.FinalBoss:
                    Console.WriteLine("CREATED ENEMY:\tRED");

                    enemy_texture = Game.spriteDict["enemyRed"];
                    return new FinalBoss(enemy_texture, GeneratePosition());
                default:
                    throw new ApplicationException(string.Format("Wrong type of enemy provided"));
            }


        }

       
    }

    
}
