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
    //Enemy Factory will now act as an abstract factory returning a generic enemy type
    public abstract class EnemyFactory
    {
        Random rand = new Random();

        public EnemyFactory() { }

        //the enemies we are going to have are going to be Grunt1 (1) Grunt2 (2) MidBoss (3) And final boss (4)


        //generate position is used in place of predetermined positions for now
        public Vector2 GeneratePosition()
        {
            int x = rand.Next(400, 1000) - 128, y = rand.Next(0, Property.ScreenHeight / 2);
            Debug.WriteLine("Position X: " + x.ToString());
            Debug.WriteLine("Position Y: " + y.ToString());
            Vector2 rand_position = new Vector2(x, y);
            return rand_position;
        }

        public abstract Enemy CreateEnemy(Vector2 position, int movementStrategy, int bulletStrategy);
    }
}
