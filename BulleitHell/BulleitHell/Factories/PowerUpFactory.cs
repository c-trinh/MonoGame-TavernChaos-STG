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

    /*
        have a random time int to chose when to spawn powerups, set in properties?
     */
    public class PowerUpFactory
    {
        public PowerUpFactory()
        {}

        public PowerUp CreateRandomPowerUp(Vector2 position)
        {
            Random rand = new Random();
            int rand_powerup = rand.Next(0, 3);
            return CreatePowerUp((Property.PowerupType)rand_powerup, position);
        }

        public PowerUp CreatePowerUp(Property.PowerupType pType, Vector2 position)
        {
            Texture2D powerup_texture = null;
            PowerUp new_powerup;
            switch (pType)
            {
                case Property.PowerupType.Health:
                    powerup_texture = Property.spriteDict["health"];
                    break;
                case Property.PowerupType.OneUp:
                    powerup_texture = Property.spriteDict["1up"];
                    break;
                case Property.PowerupType.Bomb:
                    powerup_texture = Property.spriteDict["bomb"];
                    break;
            }
            new_powerup = new PowerUp(position, (float)1, pType, powerup_texture);
            return new_powerup;
        }
    }
}
