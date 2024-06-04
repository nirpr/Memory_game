using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGameLogics
{
    internal class Player
    {
        private readonly bool r_IsComputer;
        private readonly string r_PlayerName;
        private int m_NumOfCorrectAnswers;
        private const string k_ComputerName = "Computer";
        private PlayingCards<char> m_FirstChosenLetter;

        internal Player(bool i_isComputer, string i_playerName = k_ComputerName)
        {
            r_IsComputer = i_isComputer;
            r_PlayerName = i_playerName;
        }

        public int NumOfCorrectAnswers
        {
            get
            {
                return m_NumOfCorrectAnswers;
            }
            internal set
            {
                m_NumOfCorrectAnswers += value;
            }
        }

        public string PlayerName
        {
            get
            {
                return r_PlayerName;
            }
        }

        public bool IsComputer
        {
            get
            {
                return r_IsComputer;
            }
        }

        public PlayingCards<char> FirstChosenLetter
        {
            get
            {
                return m_FirstChosenLetter;
            }
            internal set
            {
                m_FirstChosenLetter = value;
            }
        }
    }
}
