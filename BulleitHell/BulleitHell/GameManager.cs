using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace BulleitHell
{
    public class GameManager
    {
        private EnemyFactory enemyFactory;
        private EnemyFactorySelector enemySelection;
        private BulletFactory bulletFactory;
        private Interpreter interpreter;
        private PowerUpFactory powerupFactory;

        /* Relegated information used for testing purposes
        private int wave1 = 120;
        private int wave2 = 600;
        private int wave3 = 1200;
        private int wave4 = 1800;
        private int wave5 = 2400;
        private int wave6 = 3600;
        private int wave7 = 4200;
        private int wave8 = 4800;
        private int wave9 = 60;

        private int spawnInterval = 60;     // spawns an enemy every 60 frames
        private int spawnMidInterval = 10000;     // spawns an enemy every 60 frames
        private int spawnBossInterval = 15000;     // spawns an enemy every 60 frames
        private int passedMidFrames = 0;       // needed to track elapsed frames
        private int passedBossFrames = 0;       // needed to track elapsed frames
        */
        private int passedFrames = 0;       // needed to track elapsed frames
        private int spawnPowerupInterval = 500;
        private int SpawnInterval = 30; // number of frames passed before taking an enemy out of the readyqueue and adding them to the active queue
        private int passedPowerupFrames = 0;

        public bool isGameOver { private set; get; }
        public bool isGameWon { private set; get; }

        public GameManager()
        {
            enemySelection = new EnemyFactorySelector();
            bulletFactory = new BulletFactory();
            interpreter = new Interpreter();
            powerupFactory = new PowerUpFactory();
        }

        public void InitializeProperties()
        {
            // Construct players during initalization, copying in the position info
            Property.player_position = new Vector2((Engine.W_WIDTH - 128) / 2, Engine.W_HEIGHT - 128);
            Property.test_player = new Player(Property.player_position);

            // Edited by shw to construct enemy
            Property.ScreenWidth = Engine.W_WIDTH;
            Property.ScreenHeight = Engine.W_HEIGHT;

            Property.gameWon = false;

            isGameOver = false;
            isGameWon = false;

            return;
        }

        public void LoadProperties(ContentManager content)
        {
            Property.spriteDict = new Dictionary<string, Texture2D>();
            Property.SoundEffects = new Dictionary<string, SoundEffect>();

            Property.LoadSprites(content);
            Property.LoadEntities(content);
            interpreter.LoadLevel(@"..\..\..\..\interpreter\level1.json");
            Property.LoadAudio(content);

            Property.isBomb = false;

            return;
        }

        
        public PowerUp CreatePowerup(Vector2 pos)
        {
            PowerUp newPowerup = powerupFactory.CreateRandomPowerUp(pos);
            return newPowerup;
        }

        public static Enemy CreateEnemey(Vector2 pos, Property.EnemyType t, int movementStrategy, int bulletStrategy)
        {
            EnemyFactorySelector enemySelection = new EnemyFactorySelector();
            EnemyFactory enemyFactory = enemySelection.SelectFactory(t);     // enemy factory will become the necessary factory depending on what enemy type is passed in
            Enemy newEnemy = enemyFactory.CreateEnemy(pos, movementStrategy, bulletStrategy);   // A unique ID must be set in the factory
            return newEnemy;
        }

        public static void QueueEnemy(Enemy enemy)
        {
            Property.enemyQueue.Add(enemy);
        }

        private void AddEnemy()
        {
            if(Property.enemyQueue.Count > 0)
            {
                Property.enemies.Add(Property.enemyQueue.ElementAt(0));
                Property.enemyQueue.RemoveAt(0);
            }
        }
       
        public Bullet CreateBullet(Property.BulletType t, Vector2 pos, Vector2 dir, float angle, int movementPattern)
        {
            Bullet newBullet = bulletFactory.CreateBullet(t, pos, dir, angle, movementPattern);  // A unique ID must be set in the factory
            return newBullet;
        }

        public void ManagerUpdate(SpriteBatch spriteBatch, GameTime gameTime, KeyboardState state)
        {
            passedFrames++;
            passedPowerupFrames++;

            // check if user is alive
            if (Property.test_player.dead)
            {
                isGameOver = true;
                return;
            }
            
            

            
           

            Property.state = state;     // updates the keyboard state in GameProperties
            Property.test_player.Update(state);     // update the player


            //TestCode();
            //DemoCode();
            interpreter.Interpret();

            if(passedFrames >= SpawnInterval)
            {
                AddEnemy();

                passedFrames = 0;
            }

            // check if the user is shooting
            if (state.IsKeyDown(Keys.Space) && Property.prev_state.IsKeyUp(Keys.Space))
            {
                Property.playerBullets.Add(CreateBullet(Property.BulletType.Green, 
                    new Vector2(Property.test_player.entPosition.X, (Property.test_player.entPosition.Y - 100)), 
                    new Vector2(0, -1), 0, 0));
                Property.SoundEffects["throw"].Play();
            }

            /// Updates each enemy
            MovedEnemy movement = new MovedEnemy(); //strategy call
            foreach (Enemy enemy in Property.enemies)//movement updates
            {
                switch (enemy.movementStrat)
                {
                    case 1:
                        movement.SetMovement(new GruntMovement());
                        movement.Move(enemy);
                        break;

                    case 2:
                        movement.SetMovement(new GruntMovement2());
                        movement.Move(enemy);
                        break;

                    case 3:
                        movement.SetMovement(new GruntMovement3());
                        movement.Move(enemy);
                        break;

                    case 8:
                        movement.SetMovement(new MidbossMovement());
                        movement.Move(enemy);
                        break;

                    case 9:
                        movement.SetMovement(new FinalbossMovement());
                        movement.Move(enemy);
                        break;
                    default:
                        movement.SetMovement(new GruntMovement());
                        movement.Move(enemy);
                        break;

                }
            }

            if (gameTime.TotalGameTime.Milliseconds % 500 == 0)     // condition for enemies to shoot
            {
                foreach (Enemy enemy in Property.enemies)
                {
                    if (enemy.isFinBoss && gameTime.TotalGameTime.Milliseconds % 6000 == 0)
                    {
                        FinalBoss placeHolder = (FinalBoss)enemy;
                        placeHolder.FireSpecialAttack(bulletFactory);
                    }
                    Property.enemyBullets.Add(enemy.CreateBullet(Property.BulletType.Red, (float)3.15, bulletFactory));     // invoke factories in GameManager
                }
            }

            /// Updates each player bullet
            foreach (Bullet b in Property.playerBullets)
            {
                movement.SetMovement(new PlayerBulletMovement());
                movement.MoveBullet(b);
            }

            /// Updates each enemy bullet
            foreach (Bullet b in Property.enemyBullets)
            {
                switch (b.movementPattern)
                {
                    case 1:
                        movement.SetMovement(new BulletMovement());
                        movement.MoveBullet(b);
                        break;
                    case 2:
                        movement.SetMovement(new BulletMovement2());
                        movement.MoveBullet(b);
                        break;
                    case 3:
                        movement.SetMovement(new BulletMovement3());
                        movement.MoveBullet(b);
                        break;
                    default:
                        movement.SetMovement(new BulletMovement());
                        movement.MoveBullet(b);
                        break;
                }
            }

            // Updates each powerup
            foreach(var power in Property.powerUps)
            {
                power.Update();
            }

            //power up creation
            if (passedPowerupFrames >= spawnPowerupInterval)
            {
                Random rand = new Random();
                float randX = rand.Next(1, Property.ScreenWidth);
                float randY = rand.Next(1, Property.ScreenHeight);
                Vector2 pos = new Vector2(randX, randY);

                Property.powerUps.Add(CreatePowerup(pos)); // !!!
                passedPowerupFrames = 0;
            }

            Property.prev_state = Property.state;
            Property.CheckCollisions();
            Property.RemoveInactiveEntities(spriteBatch, gameTime);

            if (Property.gameWon)
            {
                isGameWon = true;
                return;
            }


            return;
        }

        public void ManagerDraw(SpriteBatch spriteBatch)
        {
            Property.DrawEntities(spriteBatch);
            Property.DrawStrings(spriteBatch);
            return;
        }
        /*
        public void TestCode()
        {
            ///                                                 ----Sample enemy creation code----   
            if (passedFrames < spawnInterval)
            {
                passedFrames++;
                passedMidFrames++;
                passedBossFrames++;
                passedPowerupFrames++;
            }

            if (passedFrames >= spawnInterval)
            {
                Random rand = new Random();
                float randX = rand.Next(1, Property.ScreenWidth);
                float randY = rand.Next(1, Property.ScreenHeight);
                Vector2 pos = new Vector2(randX, randY);

                Property.enemies.Add(CreateEnemey(pos, Property.EnemyType.Grunt2));
                passedFrames = 0;
            }
            if (passedMidFrames >= spawnMidInterval)
            {
                Random rand = new Random();
                float randX = rand.Next(1, Property.ScreenWidth);
                float randY = rand.Next(1, Property.ScreenHeight);
                Vector2 pos = new Vector2(randX, randY);

                Property.enemies.Add(CreateEnemey(pos, Property.EnemyType.MidBoss));
                passedMidFrames = 0;
            }
            if (passedBossFrames >= spawnBossInterval)
            {
                Random rand = new Random();
                float randX = rand.Next(1, Property.ScreenWidth);
                float randY = rand.Next(1, Property.ScreenHeight);
                Vector2 pos = new Vector2(randX, randY);

                Property.enemies.Add(CreateEnemey(pos, Property.EnemyType.FinalBoss));
                passedBossFrames = 0;
            }
            if (passedPowerupFrames >= spawnPowerupInterval)
            {
                Random rand = new Random();
                float randX = rand.Next(1, Property.ScreenWidth);
                float randY = rand.Next(1, Property.ScreenHeight);
                Vector2 pos = new Vector2(randX, randY);

                Property.powerUps.Add(CreatePowerup(pos)); // !!!
                passedPowerupFrames = 0;
            }
            ///                                                 ----Sample enemy creation code----   
        }

        public void DemoCode()
        {
            passedFrames++;
            passedMidFrames++;
            passedBossFrames++;
            passedPowerupFrames++;

            if (passedFrames == wave1)
            {
                float posx = Property.ScreenWidth / 2;
                float posy = 100;
                Vector2 pos = new Vector2(posx, posy);

                for (int i = 0; i < 5; i++)
                {
                    Property.enemies.Add(CreateEnemey(pos, Property.EnemyType.Grunt1));
                    posx += (float)50;
                    posy += (float)10;
                    pos = new Vector2(posx, posy);
                }
            }
            if (passedFrames == wave2)
            {
                float posx = 100;
                float posy = 100;
                Vector2 pos = new Vector2(posx, posy);
                for (int i = 0; i < 5; i++)
                {
                    Property.enemies.Add(CreateEnemey(pos, Property.EnemyType.Grunt2));
                    posx += (float)50;
                    posy += (float)10;
                    pos = new Vector2(posx, posy);
                }
            }
            if (passedFrames == wave3)
            {
                float posx = Property.ScreenWidth / 2;
                float posy = 100;
                Vector2 pos = new Vector2(posx, posy);
                for (int i = 0; i < 7; i++)
                {
                    Property.enemies.Add(CreateEnemey(pos, Property.EnemyType.Grunt1));
                    posx += (float)50;
                    posy += (float)10;
                    pos = new Vector2(posx, posy);                    
                }
            }
            if (passedFrames == wave4)
            {
                float posx = 100;
                float posy = 100;
                Vector2 pos = new Vector2(posx, posy);
                for (int i = 0; i < 7; i++)
                {
                    Property.enemies.Add(CreateEnemey(pos, Property.EnemyType.Grunt2));
                    posx += (float)50;
                    posy += (float)10;
                    pos = new Vector2(posx, posy);
                }
            }
            if (passedFrames == wave5)
            {
                float posx = Property.ScreenWidth / 2;
                float posy = 100;
                Vector2 pos = new Vector2(posx, posy);
                for (int i = 0; i < 1; i++)
                {
                    Property.enemies.Add(CreateEnemey(pos, Property.EnemyType.MidBoss));
                    posx += (float)50;
                    posy += (float)10;
                    pos = new Vector2(posx, posy);
                }
            }
            if (passedFrames == wave6)
            {
                float posx = Property.ScreenWidth / 2;
                float posy = 100;
                Vector2 pos = new Vector2(posx, posy);
                for (int i = 0; i < 7; i++)
                {
                    Property.enemies.Add(CreateEnemey(pos, Property.EnemyType.Grunt1));
                    posx += (float)50;
                    posy += (float)10;
                    pos = new Vector2(posx, posy);
                }
            }
            if (passedFrames == wave7)
            {
                float posx = 100;
                float posy = 100;
                Vector2 pos = new Vector2(posx, posy);
                for (int i = 0; i < 7; i++)
                {
                    Property.enemies.Add(CreateEnemey(pos, Property.EnemyType.Grunt2));
                    posx += (float)50;
                    posy += (float)10;
                    pos = new Vector2(posx, posy);
                }
            }
            if (passedFrames == wave8)
            {
                float posx = Property.ScreenWidth / 2;
                float posy = 100;
                Vector2 pos = new Vector2(posx, posy);
                for (int i = 0; i < 9; i++)
                {
                    Property.enemies.Add(CreateEnemey(pos, Property.EnemyType.Grunt1));
                    posx += (float)50;
                    posy += (float)10;
                    pos = new Vector2(posx, posy);
                }
            }
            if (passedFrames == wave9)
            {
                float posx = Property.ScreenWidth / 2;
                float posy = 100;
                Vector2 pos = new Vector2(posx, posy);
                for (int i = 0; i < 1; i++)
                {
                    Property.enemies.Add(CreateEnemey(pos, Property.EnemyType.FinalBoss));
                    posx += (float)50;
                    posy += (float)10;
                    pos = new Vector2(posx, posy);
                }
            }

            if (passedPowerupFrames >= spawnPowerupInterval)
            {
                Random rand = new Random();
                float randX = rand.Next(1, Property.ScreenWidth);
                float randY = rand.Next(Property.ScreenHeight/2, Property.ScreenHeight);
                Vector2 pos = new Vector2(randX, randY);

                Property.powerUps.Add(CreatePowerup(pos)); // !!!
                passedPowerupFrames = 0;
            }
        }
        */

    }
}
