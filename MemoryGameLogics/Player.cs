
namespace MemoryGameLogics
{
    internal class Player<T>
    {
        private readonly bool r_IsComputer;
        private readonly string r_PlayerName;
        private int m_NumOfCorrectAnswers;
        private const string k_ComputerName = "Computer";
        private PlayingCards<T> m_FirstChosenValue;
        private PlayingCards<T> m_SecondChosenValue;

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
                m_NumOfCorrectAnswers = value;
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

        public PlayingCards<T> FirstChosenValue
        {
            get
            {
                return m_FirstChosenValue;
            }
            internal set
            {
                m_FirstChosenValue = value;
            }
        }

        public PlayingCards<T> SecondChosenValue
        {
            get
            {
                return m_SecondChosenValue;
            }
            internal set
            {
                m_SecondChosenValue = value;
            }
        }
    }
}
