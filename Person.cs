using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//import libraries!!!
using System.IO;

namespace TextAdventure
{
    // describes the person playing
    // adding this for the TD summer camp
    class Person
    {
        // will have some redundancy at start until we remove the duplication 

        // this is the player location on the map
        Location _MyLocation;

        public string LoadSavedGame(Person PlayerIN)
        {
            //read in file;
            StreamReader sr = new StreamReader("C:\\Users\\Steve\\Source\\Repos\\OurTextBasedAdventure\\Resources\\Player.txt");
            Console.Write(".");

            // read first line,  should start with R
            string strLine = sr.ReadLine();

            sr.Close();
            sr.Dispose();

            return strLine;           
            
        }

        public void saveGame()
        {
            try
            {
                //wrap this in try catch for better experiences.
                //save a file
                StreamWriter sw = new StreamWriter("C:\\Users\\Steve\\Source\\Repos\\OurTextBasedAdventure\\Resources\\Player.txt");
                sw.WriteLine(MyLocation.getRoomIndicator());
                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public Location MyLocation { 
            get
            { return _MyLocation; }

            set
            { _MyLocation = value; }

        }

    }
}
