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
    class BulletFactory
    {

        public BulletFactory()
        { }

        public Bullet CreateBullet(Game.BulletType typeOfBullet, Vector2 dir)
        {
            Texture2D missileTexture;
            Bullet newBullet;
            switch (typeOfBullet)   // depending on the type of bullet passed, CreateBullet calls the bullet constructor passing the relevant parameters.
            {
                case Game.BulletType.Red:
                    missileTexture = Game.spriteDict["missileRed"];
                    newBullet = new Bullet(missileTexture, dir);
                    break;
                case Game.BulletType.Blue:
                    missileTexture = Game.spriteDict["missileBlue"];
                    newBullet = new Bullet(missileTexture, dir);
                    break;
                case Game.BulletType.Green:
                    missileTexture = Game.spriteDict["missileGreen"];
                    newBullet = new Bullet(missileTexture, dir);
                    break;
                case Game.BulletType.Yellow:
                    missileTexture = Game.spriteDict["missileYellow"];
                    newBullet = new Bullet(missileTexture, dir);
                    break;
                default:
                    missileTexture = Game.spriteDict["missile"];
                    newBullet = new Bullet(missileTexture, dir);
                    break;
            }

            return newBullet;
        }
    }
}
