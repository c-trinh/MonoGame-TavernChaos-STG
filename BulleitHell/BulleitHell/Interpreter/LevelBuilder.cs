using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;


namespace BulleitHell
{
    public class LevelBuilder
    {
        public LevelBuilder()
        {

        }

        public Level CreateLevel(string file)
        {
            Level newLevel = new Level(new List<Wave>());

            var level = JsonConvert.DeserializeObject<Level>(File.ReadAllText(file));

            



            return newLevel;
        }
    }
}
