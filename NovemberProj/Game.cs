using System;
using System.Collections.Generic;

namespace NovemberProjekt
{
    public class Game
    {
        public List<string> inventory = new List<string>(){};
        public List<string> antiqueStorage = new List<string>(){};


        public int CountInventory(){

            int l = inventory.Count; 
            return l;
        }
    }
}
