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
    /*     
         This is my interpretation of a player class, instatiated via inheriting from abstract entity class
    */

    class Player:Entity
    {

        //player controls to update positon
        public void Update(KeyboardState keyPress)
        {

            /// Player Speed
            if (keyPress.IsKeyDown(Keys.LeftShift))
                entSpeed = 4;
            if (!keyPress.IsKeyDown(Keys.LeftShift))
                entSpeed = 8;
            
            /// Player Movement
            if (keyPress.IsKeyDown(Keys.Right))
                entPosition.X += entSpeed;
            if (keyPress.IsKeyDown(Keys.Left))
                entPosition.X -= entSpeed;
            if (keyPress.IsKeyDown(Keys.Up))
                entPosition.Y -= entSpeed;
            if (keyPress.IsKeyDown(Keys.Down))
                entPosition.Y += entSpeed;
        }

        //basic constructor, in which we only assign initial position
        //this can obviously be changed as needed
        public Player(Vector2 pos)
        {
            entPosition = pos;
        }
    }
}
