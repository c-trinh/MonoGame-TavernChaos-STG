using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Xna.Framework;
using System.IO;
using Newtonsoft.Json;

namespace BulleitHell
{
    public class Interpreter
    {
        private Timer aTimer;
        private Level level;
        private int waveCount;

        private int enabled;

        public Interpreter()
        {

            enabled = 0;
        }

        public void LoadLevel(string fileAddress)
        {
            Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
            FileStream FILE = new FileStream(fileAddress, FileMode.Open, FileAccess.Read);
            string output = System.IO.File.ReadAllText(fileAddress);
            Console.WriteLine(output);
            Level level = JsonConvert.DeserializeObject<Level>(output);
            this.level = level;
            this.waveCount = level.waves.Count;
            foreach(var wave in level.waves)
            {
                Console.WriteLine(wave.EnemyType);
                Console.WriteLine(wave.StopWave);
            }
        }

        public void Interpret()
        {
            if (enabled == 0)
            {
                SetInterpreterTimer();
                aTimer.Start();
                enabled = 1;
            }
        }

        public void SetInterpreterTimer()
        {
            aTimer = new Timer(1000)
            {
                AutoReset = true,
                Enabled = true
            };
            aTimer.Elapsed += ReadWave;
        }

        public void ReadWave(Object source, ElapsedEventArgs e)
        {
            //Console.WriteLine("read");
            //Console.WriteLine(this.waveCount);
            while (true)
            {
                if(this.level.waves.ElementAt(0).TimeDelta == 0)
                {
                    if (this.level.waves.ElementAt(0).StopWave == true && Property.enemies.Count() > 0)
                    {
                        Console.WriteLine("test");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("test2");

                        Wave curWave = this.level.waves.ElementAt(0);
                        Vector2 pos;
                        for (int i = 0; i < curWave.EnemyCount; i++)
                        {
                            Console.WriteLine("Generating Enemy Type {0}", curWave.EnemyType);//Enemy Constructors
                            pos = new Vector2(200, 200);

                            GameManager.QueueEnemy((GameManager.CreateEnemey(pos, (Property.EnemyType)curWave.EnemyType)));
                        }
                        


                        this.level.waves.RemoveAt(0);
                        this.waveCount -= 1;
                        Console.WriteLine(this.waveCount);

                        if (this.waveCount == 0)
                        {
                            Console.WriteLine("Check");
                            aTimer.Enabled = false;
                            aTimer.Stop(); 
                            break;
                        }
                    }
                }
                else
                {
                    this.level.waves.ElementAt(0).TimeDelta -= 1;
                    break;
                }
            }



        }
    }
}
