using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulleitHell.Entities
{
    class ExternalBulletDecorator
    {
        BulletFactory BulletProducer;
        public Bullet DecoratedBullet { get; private set; }
        // special bullet properties

        public ExternalBulletDecorator(BulletFactory b)
        {
            BulletProducer = b;
            DecorateBullet();
        }

        private void DecorateBullet()
        {
            // pass in special bullet properties to factory and save the returned bullet to DecoratedBullet

        }
    }
}
