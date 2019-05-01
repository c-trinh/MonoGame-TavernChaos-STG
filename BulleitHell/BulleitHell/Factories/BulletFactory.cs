using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BulleitHell
{
    public class BulletFactory
    {

        public BulletFactory()
        { }

        public Bullet CreateBullet(Property.BulletType typeOfBullet, Vector2 position, Vector2 direction, float angle, int movementPattern)
        {
            Texture2D missileTexture;
            Bullet newBullet;
            bool is_spin = false;
            switch (typeOfBullet)
            {
                case Property.BulletType.Red:
                    missileTexture = Property.spriteDict["missileRed"];
                    break;
                case Property.BulletType.Blue:
                    missileTexture = Property.spriteDict["missileBlue"];
                    break;
                case Property.BulletType.Green:
                    missileTexture = Property.spriteDict["missileGreen"];
                    is_spin = true;
                    break;
                case Property.BulletType.Yellow:
                    missileTexture = Property.spriteDict["missileYellow"];
                    break;
                case Property.BulletType.Special:
                    missileTexture = Property.spriteDict["specialFist"];
                    newBullet = new Bullet(missileTexture, position, direction, angle, false, movementPattern);
                    newBullet.size = 100;
                    newBullet.entSpeed = 12;
                    newBullet.hitbox.Width = 96;
                    newBullet.hitbox.Height = 96;
                    newBullet.hitbox.X += 64;
                    newBullet.hitbox.Y += 48;
                    return newBullet;
                default:
                    missileTexture = Property.spriteDict["missile"];
                    break;
            }
            newBullet = new Bullet(missileTexture, position, direction, angle, is_spin, movementPattern);
            return newBullet;
        }
    }
}
