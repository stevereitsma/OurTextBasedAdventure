using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{    
	class Item
	{

        public string name;
        private bool useable;
        private bool needsItem;
        private string description;
	
		public Item ( string _name, bool canUse, string _description)
		{
            name = _name;
            useable = canUse;
            description = _description;
		}

        public Item (string MapFileString)
        {
            string[] MFS = MapFileString.Split(',');
            name = MFS[1];
            useable = bool.Parse(MFS[2]);
            description = MFS[3];
        }

        public string Name
        {
            get { return name; }
        }

        public bool Useable
        {
            get { return useable; }
        }

        public string Description
        {
            get { return description; }
        }
	}
}
