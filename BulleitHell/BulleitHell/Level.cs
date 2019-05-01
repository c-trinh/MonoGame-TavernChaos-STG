using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulleitHell
{
    public class Level
    {
        public List<Wave> waves = new List<Wave>();

        public Level(List<Wave> waveList)
        {
            waves = waveList;
        }

        public void AddWave(Wave newWave)
        {
            waves.Add(newWave);
        }
    }
}
