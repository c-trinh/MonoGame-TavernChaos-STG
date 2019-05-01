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
    class midbossFactory : EnemyFactory
    {
        public override Enemy CreateEnemy(Vector2 pos, int movementStrategy, int bulletStrategy)
        {
            Enemy NewEnemy = new MidBoss(pos, (float)1.5, movementStrategy, bulletStrategy);
            //GeneratePosition is used temporarily while we do not have positions to pass in
            return NewEnemy;
        }
    }
}
