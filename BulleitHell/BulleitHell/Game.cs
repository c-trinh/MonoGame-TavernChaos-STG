using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BulleitHell
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Texture2D bg_sky;
        private Texture2D player;
        private Texture2D enemy;
        private Texture2D final_boss;
        private Texture2D grunt_2;
        private Texture2D grunt_1;
        private Texture2D mid_boss;

        private Texture2D missile;
        private Texture2D missile_red;
        private Texture2D missile_blue;
        private Texture2D missile_green;
        private Texture2D missile_yellow;
        private SpriteFont font;

        public enum EnemyType { Grunt1, Grunt2, MidBoss, FinalBoss };
        public enum BulletType { Red, Green, Blue, Yellow };
        public static Dictionary<string, Texture2D> spriteDict;
        Random rand = new Random();

        //position 2 is for testing, can be removed later
        Vector2 player_position;

        //two player objects for testing updating/drawing multiple objects
        Player test;
        //Enemy testEnemy;
        List<Enemy> enemies = new List<Enemy>(); //list of enemies
        List<Bullet> bullets = new List<Bullet>();

        private readonly int W_WIDTH = 1280;
        private readonly int W_HEIGHT = 800;

        private int score = 0;

        public int Score { get => score; set => score = value; }

        public Game()
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
            /// TODO: Add your initialization logic here
            player_position = new Vector2((W_WIDTH - 128) / 2, W_HEIGHT - 128);

            // construct players during initalization, copying in the position info
            test = new Player(player_position);

            // edited by shw to construct enemy
            GameProperties.ScreenWidth = W_WIDTH;
            GameProperties.ScreenHeight = W_HEIGHT;

            

            GameProperties.ScreenWidth = W_WIDTH;
            GameProperties.ScreenHeight = W_HEIGHT;

            base.Initialize();
        }

        /// LoadContent called once and is the place to load all of your content.
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteDict = new Dictionary<string, Texture2D>();
            bg_sky = this.Content.Load<Texture2D>("Sprites/sky");
            player = this.Content.Load<Texture2D>("Sprites/player");
            enemy = this.Content.Load<Texture2D>("Sprites/enemy");
            grunt_1 = this.Content.Load<Texture2D>("Sprites/enemy_green");
            grunt_2 = this.Content.Load<Texture2D>("Sprites/enemy_blue");
            mid_boss = this.Content.Load<Texture2D>("Sprites/enemy_yellow");
            final_boss = this.Content.Load<Texture2D>("Sprites/enemy_red");
            missile = this.Content.Load<Texture2D>("Sprites/missile");
            missile_red = this.Content.Load<Texture2D>("Sprites/missile_red");
            missile_blue = this.Content.Load<Texture2D>("Sprites/missile_blue");
            missile_green = this.Content.Load<Texture2D>("Sprites/missile_green");
            missile_yellow = this.Content.Load<Texture2D>("Sprites/missile_yellow");

            spriteDict.Add("player", player);
            spriteDict.Add("sky", bg_sky);
            spriteDict.Add("enemy", enemy);
            spriteDict.Add("enemyGreen", grunt_1);
            spriteDict.Add("enemyBlue", grunt_2);
            spriteDict.Add("enemyYellow", mid_boss);
            spriteDict.Add("enemyRed", final_boss);
            spriteDict.Add("missile", missile);
            spriteDict.Add("missileRed", missile_red);
            spriteDict.Add("missileBlue", missile_blue);
            spriteDict.Add("missileGreen", missile_green);
            spriteDict.Add("missileYellow", missile_yellow);

            //pass in the loaded texture to be stored in the object, so we can load each texture only once
            test.Load(player);

            EnemyFactory factory = new EnemyFactory();
            var numberOfEnemies = 10;

            while (enemies.Count < numberOfEnemies)
            {
                int rand_type = rand.Next(0, 4);
                enemies.Add(factory.CreateEnemy((EnemyType)rand_type));
            }

            font = Content.Load<SpriteFont>("Fonts/arialblack");
        }

        /// UnloadContent will be called once per game and is the place to unload game-specific content.
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// Allows the game to run logic such as updating the world, checking for collisions, gathering input, and playing audio.
        protected override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Escape))
                Exit();

            // Wrapped update logic into this function, only requires keyboard state
            test.Update(state);

            /// Updates each enemy
            for (var i = 0; i < enemies.Count; i++)
            {
                enemies[i].Update();
            }

            if (gameTime.TotalGameTime.Milliseconds % 500 == 0)
            {
                foreach (Enemy enemy in enemies)
                {
                    bullets.Add(enemy.Shoot());
                }
            }

            /// Updates each bullet
            foreach (Bullet b in bullets)
            {
                b.Update();
            }

            /// Increments the Score when Enemy is destroyed
            if (state.IsKeyDown(Keys.Space))
                score++;

            base.Update(gameTime);
        }

        /// This is called when the game should draw itself.
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            // Draws Sprites
            spriteBatch.Draw(bg_sky, new Rectangle(0, 0, W_WIDTH, W_HEIGHT), Color.White);
            // spriteBatch.Draw(player, player_position, Color.White);

            // just pass in the sprite batch to make the draw call, but begin and end call happen in this loop
            test.Draw(spriteBatch);

            for (var i = 0; i < enemies.Count; i++)
            {
                enemies[i].Draw(spriteBatch);
            }

            foreach (Bullet bullet in bullets)
            {
                bullet.Draw(spriteBatch);
            }

            //spriteBatch.Draw(missile, new Vector2((W_WIDTH-96)/2, (W_HEIGHT-96)/2), Color.White);
            spriteBatch.DrawString(font, "Press SPACE to increase SCORE.", new Vector2(30, 20), Color.Black);
            spriteBatch.DrawString(font, "Press ESC to Exit.", new Vector2(30, 50), Color.Black);
            spriteBatch.DrawString(font, "SCORE: " + score.ToString(), new Vector2(W_WIDTH - 250, 20), Color.Red);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
