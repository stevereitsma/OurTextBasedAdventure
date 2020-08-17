using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class Exit
    {
		public enum Directions
		{
			Undefined, North, South, East, West, Up, Down, NorthEast, NorthWest, SouthEast, SouthWest, In, Out
		};

		public static string[] shortDirections = {"Null", "N", "S", "E", "W", "U", "D", "NE", "NW", "SE", "SW", "I", "O"};

		private string leadsTo;
		private string direction;
		private string shortDirection;

		public Exit()
		{
			direction = "Undefined";
			leadsTo = "Null";
		}

		public Exit(String _direction, String newLeadsTo)
		{
			direction = _direction;
			leadsTo = newLeadsTo;
		}

		public Exit(string MapFileString)
		{
			string[] MFS = MapFileString.Split(',');
			//string strDirection, strLeadsTo;
			direction = MFS[1];
			leadsTo = MFS[3];
			shortDirection = MFS[2];
			
		}

		public override string ToString()
		{
			return direction.ToString();
		}

		public void setDirection(string _direction)
		{
			direction = _direction;
		}

		public string getDirection()
		{
			return direction;
		}

		public string getShortDirection()
		{
			return shortDirection;
		}

		public void setLeadsTo(string _leadsTo)
		{
			leadsTo = _leadsTo;
		}

		public string getLeadsTo()
		{
			return leadsTo;
		}
    }
}
