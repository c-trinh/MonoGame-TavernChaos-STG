using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulleitHell
{
    public class Wave
    {
        public int WaveId;
        public int TimeDelta;
        public int EnemyType;
        public int EnemyCount;
        public int Speed;
        public int MovementStrategy;
        public int BulletStrategy;
        public bool StopWave;
        public int XPos;
        public int YPos;



        public Wave(int WaveId, int TimeDelta, int EnemyType, int Speed, int MovementStrategy, int BulletStrategy, int EnemyCount, bool StopWave, int XPosition, int YPosition)
        {
            this.WaveId = WaveId;
            this.TimeDelta = TimeDelta;
            this.EnemyType = EnemyType;
            this.EnemyCount = EnemyCount;
            this.Speed = Speed;
            this.MovementStrategy = MovementStrategy;
            this.BulletStrategy = BulletStrategy;
            this.EnemyCount = EnemyCount;
            this.StopWave = StopWave;
            this.XPos = XPosition;
            this.YPos = YPosition;
        }
    }
}
