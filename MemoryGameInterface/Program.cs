﻿using System;
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
            GameInterface gameInterface = new GameInterface();
            gameInterface.StartGame();
        }
    }
}
