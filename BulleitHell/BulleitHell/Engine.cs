using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace BulleitHell
{
    public class Engine : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;        // Used to draw/display sprites onto screen

        Random rand = new Random();


        public static readonly int W_WIDTH = 1280;
        public static readonly int W_HEIGHT = 800;

        private GameManager gameManager;

        public Engine()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = W_WIDTH,     // Window Width-Size
                PreferredBackBufferHeight = W_HEIGHT    // Window Height-Size
            };
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        /// Allows the game to perform any initialization it needs to before starting to run.
        protected override void Initialize()
        {
            gameManager = new GameManager();
            gameManager.InitializeProperties();
            base.Initialize();
        }

        /// LoadContent called once and is the place to load all of your content.
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameManager.LoadProperties(Content);
        }

        /// UnloadContent will be called once per game and is the place to unload game-specific content.
        protected override void UnloadContent()
        {

        }

        /// Allows the game to run logic such as updating the world, checking for collisions, gathering input, and playing audio.
        protected override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Escape))   // exit is a function of the view so it must be called here
            {
                Exit();
            }

            gameManager.ManagerUpdate(spriteBatch, gameTime, state);
            base.Update(gameTime);
        }

        /// This is called when the game should draw itself.
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            if (gameManager.isGameWon)
            {
                spriteBatch.DrawString(Property.font, "YOU WIN", new Vector2((float)(W_WIDTH * .5), (float)(W_HEIGHT * .5)), Color.White);
                spriteBatch.DrawString(Property.font, "PRESS ESC TO QUIT", new Vector2((float)(W_WIDTH * .5), (float)(W_HEIGHT * .5) + 30), Color.White);
            }
            else if (!gameManager.isGameOver)
            {
                gameManager.ManagerDraw(spriteBatch);
            }
            else
            {
                spriteBatch.DrawString(Property.font, "GAME OVER", new Vector2((float)(W_WIDTH * .5), (float)(W_HEIGHT * .5)), Color.White);
                spriteBatch.DrawString(Property.font, "PRESS ESC TO QUIT", new Vector2((float)(W_WIDTH * .5), (float)(W_HEIGHT * .5) + 30), Color.White);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void ExitGame()
        {
            Exit();
        }
    }
}
