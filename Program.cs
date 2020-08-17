using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class Program
    {
        static void Main(string[] args)
        {
            // look at args to determine run mode...
            bool isDebug = false;

            if (args != null && args.Length > 0)
            {
                string p1 = args[0];
                if (p1.ToUpper() == "DEBUG") isDebug = true;
            }

            Game _Game = new Game(isDebug);


            //start our game loop - we keep running this function until the player quits.
            while (_Game.isRunning)
            {
                _Game.Update();
            }
        }
    }
}
