using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;

namespace TextAdventure
{
    class Game
    {
        Location currentLocation;

        public bool isRunning = true;
        private bool _gameOver = false;

        private List<Item> inventory;
        private List<Location> Map;

        public Game()
        {
            inventory = new List<Item>();
            Map = new List<Location>();

            Console.WriteLine("Welcome adventurer, prepare yourself for a fantastical journey into the unknown.");
            Console.WriteLine("opening file for map");

            readMapFile();

            Console.WriteLine("Press 'h' or type 'help' for help.");

            currentLocation = Map.ElementAt(0);

            showLocation();

        }



        private void readMapFile()
        {
            string line;
            try
            {
                StreamReader sr = new StreamReader("C:\\Users\\Steve\\Source\\Repos\\OurTextBasedAdventure\\Resources\\consoleMap.txt");


                // read first line,  should start with R
                line = sr.ReadLine();

                // read until end of file;
                while (line != null)
                {
                    // this is room
                    Console.WriteLine(line);
                    Location tempLocation = new Location(line);
                    line = sr.ReadLine();


                    // read next lines until not I
                    line = sr.ReadLine();
                    while (line.StartsWith("I"))
                    {
                        Console.WriteLine(line);
                        Item tempItem = new Item(line);
                        tempLocation.addItem(tempItem);

                        line = sr.ReadLine();
                    }
                    // read next line until not E
                    while (line != null && line.StartsWith("E"))
                    {
                        Console.WriteLine(line);
                        Exit tempexit = new Exit(line);
                        tempLocation.addExit(tempexit);
                        line = sr.ReadLine();
                    }

                    Map.Add(tempLocation);

                    // move to next room or end of file.

                }
            }
            catch (FileNotFoundException fnfe)
            {
                Console.WriteLine("file was not found");
                Console.WriteLine(fnfe.Message);
            }
            catch (DirectoryNotFoundException dnfe)
            {
                Console.WriteLine("directory was not found");
                Console.WriteLine(dnfe.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("something unexpected happened");
                Console.WriteLine(ex.Message);
            }

        }
        public void showLocation()
        {
            Console.WriteLine("\n" + currentLocation.getTitle() + "\n");
            Console.WriteLine(currentLocation.getDescription());

            if (currentLocation.getInventory().Count > 0)
            {
                Console.WriteLine("\nThe room contains the following:\n");

                for (int i = 0; i < currentLocation.getInventory().Count; i++)
                {
                    Console.WriteLine(currentLocation.getInventory()[i].Name);
                }
            }

            Console.WriteLine("\nAvailable Exits: \n");

            foreach (Exit exit in currentLocation.getExits())
            {
                Console.WriteLine(exit.getDirection());
            }

            Console.WriteLine();
        }

        // TODO: Implement the input handling algorithm.
        public void doAction(string command)
        {
            //Help command is NEW
            if (command == "help" || command == "h")
            {
                Console.WriteLine("Welcome to this Text Adventure!");
                Console.WriteLine("'l' / 'look':        Shows you the room, its exits, and any items it contains.");
                Console.WriteLine("'Look at X':         Gives you information about a specific item in your \n                     inventory, where X is the items name.");
                Console.WriteLine("'pick up X':         Attempts to pick up an item, where X is the items name.");
                Console.WriteLine("'use X':             Attempts to use an item, where X is the items name.");
                Console.WriteLine("'i' / 'inventory':   Allows you to see the items in your inventory.");
                Console.WriteLine("'q' / 'quit':        Quits the game.");
                Console.WriteLine();
                Console.WriteLine("Directions can be input as either the full word, or the abbriviation, \ne.g. 'North or N'");
                return;
            }

            //If statement to access the player inventory
            //This can't be changed a great deal
            if (command == "inventory" || command == "i")
            {
                showInventory();
                Console.WriteLine();
                return;
            }

            //If statement for player to pick up objects
            //This works fine. Change how the function works later though.
            if (command.Length >= 7 && command.Substring(0, 7) == "pick up")
            {
                if (command == "pick up")
                {
                    Console.WriteLine("\nPlease specify what you would like to pick up.\n");
                    return;
                }
                if (currentLocation.getInventory().Exists(x => x.Name == command.Substring(8)) && currentLocation.getInventory().Exists(x => x.Useable == true))
                {
                    inventory.Add(currentLocation.takeItem(command.Substring(8)));
                    Console.WriteLine("\nYou pick up the " + command.Substring(8) + ".\n");
                    return;
                }
                if (currentLocation.getInventory().Exists(x => x.Name == command.Substring(8)) && currentLocation.getInventory().Exists(x => x.Useable == false))
                {
                    Console.WriteLine("\nYou cannot pick up the " + command.Substring(8) + ".\n");
                    return;
                }
                else
                {
                    Console.WriteLine("\n" + command.Substring(8) + " does not exist.\n");
                    return;
                }
            }

            if (command == "look" || command == "l")
            {
                showLocation();
                if (currentLocation.getInventory().Count == 0)
                {
                    Console.WriteLine("There are no items of interest in the room.\n");
                }
                return;
            }

            if (command.Length >= 7 && command.Substring(0, 7) == "look at")
            {
                if (command == "look at")
                {
                    Console.WriteLine("\nPlease specify what you wish to look at.\n");
                    return;
                }
                if (currentLocation.getInventory().Exists(x => x.Name == command.Substring(8)) || inventory.Exists(x => x.Name == command.ToLower().Substring(8)))
                {
                    if (command.Substring(8).ToLower() == "rock")
                    {
                        Console.WriteLine("\n" + currentLocation.getInventory().Find(x => x.Name.Contains("rock")).Description + "\n");
                        return;
                    }

                    if (command.Substring(8).ToLower() == "window")
                    {
                        Console.WriteLine("\n" + currentLocation.getInventory().Find(x => x.Name.Contains("window")).Description + "\n");
                        return;
                    }

                    if (command.Substring(8).ToLower() == "smashed window")
                    {
                        Console.WriteLine("\n" + currentLocation.getInventory().Find(x => x.Name.Contains("smashed window")).Description + "\n");
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("\nThat item does not exist in this location or your inventory.\n");
                    return;
                }
            }

            if (command.Length >= 3 && command.Substring(0, 3) == "use")
            {
                if (command == "use")
                {
                    Console.WriteLine("\nPlease specify what you wish to use.\n");
                    return;
                }
                if (inventory.Exists(x => x.Name == command.ToLower().Substring(4)))
                {
                    Console.WriteLine("\nUse " + command.Substring(4) + " with?\n");
                    string secondItem = Console.ReadLine();
                    if (currentLocation.getInventory().Exists(x => x.Name == secondItem))
                    {
                        if (secondItem == "window" && command.Substring(4) == "rock")
                        {
                            Item smashedWindow = new Item("smashed window", false, "A window frame with broken pieces of glass inside.");
                            currentLocation.addItem(smashedWindow);
                            foreach (Item item in currentLocation.getInventory())
                            {
                                if (item.Name.ToLower() == "window")
                                {
                                    currentLocation.removeItem(item);
                                    break;
                                }

                                if (item.Name.ToLower() == "rock")
                                {
                                    currentLocation.removeItem(item);
                                    break;
                                }

                            }
                            Console.WriteLine("\nYou smash in the window.\n");
                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Cannot do the thing.");
                        return;
                    }
                }
                if (currentLocation.getInventory().Exists(x => x.Name == command.ToLower().Substring(4)))
                {
                    if (command.ToLower().Substring(4) == "window")
                    {
                        Console.WriteLine("\nThe window's locked tight, with no way of opening it.\n");
                        return;
                    }
                    if (command.ToLower().Substring(4) == "smashed window")
                    {
                        Console.WriteLine("\nYou clamber out the window. Victory is yours!");
                        _gameOver = true;
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("\nThere is nothing to use.\n");
                    return;
                }
            }

            if (moveRoom(command))
                return;

            Console.WriteLine("\nInvalid command, are you confused?\n");
        }

        private bool moveRoom(string command)
        {
            foreach (Exit exit in currentLocation.getExits())
            {
                if (command == exit.ToString().ToLower() || command == exit.getShortDirection().ToLower())
                {
                    //currentLocation = exit.getLeadsTo();
                    currentLocation = getLocationInMap(exit.getLeadsTo());
                    Console.WriteLine("\nYou move " + exit.ToString() + " to the:\n");
                    showLocation();
                    return true;
                }
            }
            return false;
        }

        private Location getLocationInMap(string newLocation)
        {
            Location newLC = new Location();

            foreach (Location lc in Map )
            {
                if ( lc.getRoomIndicator() == newLocation)
                {
                    newLC = lc;
                    break;
                }
            }


            return newLC;
        }


        private void showInventory()
        {
            if (inventory.Count > 0)
            {
                Console.WriteLine("\nA quick look in your bag reveals the following:\n");

                foreach (Item item in inventory)
                {
                    Console.WriteLine(item.Name);
                }
            }
            else
            {
                Console.WriteLine("\nYour bag is empty.\n");
            }
        }

        public void Update()
        {
            string currentCommand = Console.ReadLine().ToLower();

            // instantly check for a quit
            if (currentCommand == "quit" || currentCommand == "q")
            {
                isRunning = false;
                return;
            }


            if (!_gameOver)
            {
                // otherwise, process commands.
                doAction(currentCommand);
            }
            else
            {
                Console.WriteLine("\nNope. Time to quit.\n");
            }
        }
    }
}
