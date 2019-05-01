using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace BulleitHell
{
    public static class Property
    {
        public static int ScreenWidth { get; set; }
        public static int ScreenHeight { get; set; }

        public static Song theme_audio;
        //public static List<SoundEffect> soundEffects;
        public static bool isBomb { set; get; }

        public static Dictionary<string, Texture2D> spriteDict { get; internal set; }
        public static Dictionary<string, SoundEffect> SoundEffects { get; internal set; }

        public static List<Enemy> enemies = new List<Enemy>();    // List of Enemies
        public static List<Enemy> enemyQueue = new List<Enemy>(); //list of enemies to be added to the enemies list

        public static List<Bullet> inactiveEnemyBullets = new List<Bullet>();//list of inactive bullets to remove for enemies
        public static List<Bullet> inactivePlayerBullets = new List<Bullet>();//list of inactive bullets to remove for player
        // lists for player and enemies to process bullet entities uniquely
        public static List<Enemy> inactiveEnemies = new List<Enemy>(); //list of dead enemies to remove on update calls
        public static List<PowerUp> inactivePowerUps = new List<PowerUp>();

        public static List<Bullet> enemyBullets = new List<Bullet>();  // List of enemy Bullets
        public static List<Bullet> playerBullets = new List<Bullet>();  // List of player Bullets
        public static List<PowerUp> powerUps = new List<PowerUp>();     // List of Powerups

        public static Player test_player;

        private static int bomb_interval = 0;

        public static bool gameWon;

        public enum EnemyType { Grunt1, Grunt2, MidBoss, FinalBoss };
        public enum PowerupType { Health, OneUp, Bomb };
        public enum BulletType { Red, Green, Blue, Yellow, Special };

        public const int GRUNT1 = 80;
        public const int GRUNT2 = 81;
        public const int MIDBOSS = 85;
        public const int FINALBOSS = 88;

        private static int score = 0;
        public static int SCORE { get => score; set => score = value; }
        public static Vector2 player_position { get; internal set; }

        public static Texture2D background;
        public static Texture2D player;
        public static Texture2D enemy;
        public static Texture2D final_boss;
        public static Texture2D grunt_2;
        public static Texture2D grunt_1;
        public static Texture2D mid_boss;

        public static Texture2D missile;
        public static Texture2D missile_red;
        public static Texture2D missile_blue;
        public static Texture2D missile_green;
        public static Texture2D missile_yellow;

        public static Texture2D powerup_health;
        public static Texture2D powerup_1up;
        public static Texture2D powerup_bomb;

        public static Texture2D special_fist;
        public static SpriteFont font;

        public static KeyboardState state;
        public static KeyboardState prev_state;

        public static void LoadSprites(ContentManager Content)
        {
            background = Content.Load<Texture2D>("Sprites/floor");
            player = Content.Load<Texture2D>("Sprites/player");
            spriteDict.Add("player", Content.Load<Texture2D>("Sprites/player"));
            spriteDict.Add("player_wasted", Content.Load<Texture2D>("Sprites/player_wasted"));
            spriteDict.Add("sky", Content.Load<Texture2D>("Sprites/bottle"));
            spriteDict.Add("enemy", Content.Load<Texture2D>("Sprites/bottle"));
            spriteDict.Add("grunt_1", Content.Load<Texture2D>("Sprites/bottle"));
            spriteDict.Add("grunt_2", Content.Load<Texture2D>("Sprites/bottle"));
            spriteDict.Add("mid_boss", Content.Load<Texture2D>("Sprites/bartender"));
            spriteDict.Add("final_boss", Content.Load<Texture2D>("Sprites/bouncer"));
            spriteDict.Add("missile", Content.Load<Texture2D>("Sprites/shot"));
            spriteDict.Add("missileRed", Content.Load<Texture2D>("Sprites/alchohol"));
            spriteDict.Add("missileBlue", Content.Load<Texture2D>("Sprites/alchohol"));
            spriteDict.Add("missileGreen", Content.Load<Texture2D>("Sprites/shot"));
            spriteDict.Add("missileYellow", Content.Load<Texture2D>("Sprites/fist"));
            spriteDict.Add("health", Content.Load<Texture2D>("Sprites/bucket"));
            spriteDict.Add("1up", Content.Load<Texture2D>("Sprites/burger"));
            spriteDict.Add("bomb", Content.Load<Texture2D>("Sprites/tray"));
            spriteDict.Add("wave", Content.Load<Texture2D>("Sprites/wave"));
            special_fist = Content.Load<Texture2D>("Sprites/specialFist");

            spriteDict.Add("specialFist", special_fist);
            font = Content.Load<SpriteFont>("Fonts/arialblack");
        }

        public static void LoadAudio(ContentManager Content)
        {
            theme_audio = Content.Load<Song>("Audio/theme");

            SoundEffects.Add("throw", Content.Load<SoundEffect>("Audio/throw"));
            SoundEffects.Add("break", Content.Load<SoundEffect>("Audio/break"));
            SoundEffects.Add("hurt", Content.Load<SoundEffect>("Audio/hurt"));
            SoundEffects.Add("powerup", Content.Load<SoundEffect>("Audio/powerup"));
            SoundEffects.Add("vomit", Content.Load<SoundEffect>("Audio/vomit"));
            SoundEffects.Add("munch", Content.Load<SoundEffect>("Audio/munch"));
            SoundEffects.Add("wave", Content.Load<SoundEffect>("Audio/wave"));

            MediaPlayer.Play(Property.theme_audio);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume -= 0.2f;
        }

        public static void LoadEntities(ContentManager Content)
        {
            test_player.Load(Property.player);
            font = Content.Load<SpriteFont>("Fonts/arialblack");
        }

        public static void CheckCollisions()
        {
            //for all enemies
            //for all bulllets
            //check for players

            bool playerHit = false; //status flag if the player has been hit

            foreach (var enemy in enemies)
            {
                if (enemy.hitbox.Intersects(test_player.hitbox))
                {
                    test_player.TakeDamage();
                    SoundEffects["hurt"].Play();
                }

                foreach (var bullet in enemyBullets)
                {
                    if (bullet.hitbox.Intersects(test_player.hitbox)) //check bullet hits player
                    {
                        playerHit = test_player.TakeDamage();
                        inactiveEnemyBullets.Add(bullet);
                        SoundEffects["hurt"].Play();
                    }
                    if (bullet.size == 100)
                    {
                        foreach (var subBullet in enemyBullets)
                        {
                            if (bullet.hitbox.Intersects(subBullet.hitbox))
                            {
                               subBullet.entSpeed = 12;
                            }
                        }
                    }
                }

                foreach (var bullet in playerBullets)
                {
                    if (bullet.hitbox.Intersects(enemy.hitbox)) //check bullet hits enemies
                    {
                        //Console.WriteLine("Enemy hit by bullet");
                        enemy.TakeDamage();
                        if (enemy.active == 0)
                        {
                            inactiveEnemies.Add(enemy); //add dead enemy to remove list
                        }
                        inactivePlayerBullets.Add(bullet);
                        SoundEffects["break"].Play();
                        SCORE++;
                    }
                }

            

            //if our player has been hit we clear all bullets
            if (playerHit)
                Property.ClearBullets();
            //maybe add bomb statement here?
            }
            foreach (var power in powerUps)
            {
                if (power.hitbox.Intersects(test_player.hitbox))
                {
                    if (power.active == 1)
                    {
                        test_player.RecievePowerup(power.GivePower());
                        inactivePowerUps.Add(power);
                        SoundEffects["powerup"].Play();
                    }
                }
            }
        }

        public static void DrawStrings(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Property.font, "Press SPACE to SHOOT.", new Vector2(30, 20), Color.Black);
            spriteBatch.DrawString(Property.font, "Press ESC to Exit.", new Vector2(30, 50), Color.Black);
            spriteBatch.DrawString(Property.font, "SCORE:  " + Property.SCORE.ToString(), new Vector2(ScreenWidth - 350, 100), Color.Blue);
            spriteBatch.DrawString(Property.font, "HEALTH: " + Property.test_player.GetHealth().ToString(), new Vector2(ScreenWidth - 350, 60), Color.Red); //display lives from player
            spriteBatch.DrawString(Property.font, "LIVES:  " + Property.test_player.GetLives().ToString(), new Vector2(ScreenWidth - 350, 20), Color.Green); //display lives from player
        }

        public static void ClearBullets()
        {
            enemyBullets.Clear();
            Console.WriteLine("bullets cleared on hit");
        }

        public static void RemoveInactiveEntities(SpriteBatch spriteBatch, GameTime gameTime)       // removes all inactive entities
        {
            foreach(Enemy inactive in inactiveEnemies)
            {
                if (inactive.isFinBoss)
                {
                    gameWon = true;
                }
                enemies.Remove(inactive);
            }
            foreach(var inactive in inactiveEnemyBullets)
            {
                enemyBullets.Remove(inactive);
            }
            foreach (var inactive in inactivePlayerBullets)
            {
                playerBullets.Remove(inactive);
            }
            foreach(var inactive in inactivePowerUps)
            {
                powerUps.Remove(inactive);
            }
            inactiveEnemyBullets.Clear();
            inactivePlayerBullets.Clear();
            inactiveEnemies.Clear();
            inactivePowerUps.Clear();
        }

        public static void DrawEntities(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.White);

            if (test_player.GetLives() != "Dead") //if the player is out of lives, they dissapear
                test_player.Draw(spriteBatch);

            /// Updates each enemy
            for (var i = 0; i < enemies.Count; i++)
            {
                enemies[i].Draw(spriteBatch);
            }

            foreach (Bullet bullet in enemyBullets)
            {
                bullet.Draw(spriteBatch);
            }

            foreach (Bullet bullet in playerBullets)
            {
                bullet.Draw(spriteBatch);
            }

            foreach(var power in powerUps)
            {
                power.Draw(spriteBatch);
            }

            if (Property.isBomb)
            {
                //spriteBatch.Draw(spriteDict["wave"], new Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.White);
                spriteBatch.Draw(spriteDict["wave"], new Rectangle(ScreenWidth / 2, ScreenHeight / 2, ScreenWidth*2, ScreenHeight*2),
                    null, Color.White, 0.01f * bomb_interval, new Vector2(ScreenWidth / 2f, ScreenHeight / 2f), SpriteEffects.None, 0f);
                bomb_interval++;
                if (bomb_interval == 60)
                {
                    bomb_interval = 0;
                    Property.isBomb = false;
                }
            }
        }
    }
}
