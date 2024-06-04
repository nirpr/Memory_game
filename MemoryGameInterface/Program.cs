using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemoryGameLogics;


namespace MemoryGameInterface
{
    public class Program
    {
        public static void Main()
        {
            // this Main is just for checks.
            List<string> lst = new List<string>();
            lst.Add("Nir");
            lst.Add("Omer");
            GamePlay gameplay = new GamePlay(lst, 4, 4);
            gameplay.firstCellChosenByPlayer(2, 3);
            gameplay.secondCellChosenByPlayer(1, 2);
            gameplay.firstCellChosenByPlayer(1, 2);
            gameplay.secondCellChosenByPlayer(0, 3);
        }
    }
}
