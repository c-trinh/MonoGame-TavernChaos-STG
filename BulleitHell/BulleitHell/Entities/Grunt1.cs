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
    class Grunt1 : Enemy
    {
       public Grunt1(Vector2 pos, int movementStrategy, int bulletStrategy) : base(pos, (float)1.15, movementStrategy, bulletStrategy)
       {
            Console.WriteLine("In G1 Constructor");
            entTexture = Property.spriteDict["grunt_1"];
            health = 1; //placeholder value, may update
            isFinBoss = false;
        }
    }
}
