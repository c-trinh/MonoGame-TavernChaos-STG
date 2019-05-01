using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulleitHell
{
    class EnemyFactorySelector
    {
        public EnemyFactorySelector()
        {
        }
        //Macros should be defined for each of the enemy types
        public EnemyFactory SelectFactory(Property.EnemyType type)
        {
            switch(type)
            {
                case Property.EnemyType.Grunt1:
                    {
                        return new grunt1Factory();
                    }
                case Property.EnemyType.Grunt2:
                    {
                        return new grunt2Factory();

                    }
                case Property.EnemyType.MidBoss:
                    {
                        return new midbossFactory();

                    }
                case Property.EnemyType.FinalBoss:
                    {
                        return new finalbossFactory();
                    }
                default:
                    return new grunt1Factory();
            }
        }
    }
}
