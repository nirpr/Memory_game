using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGameLogics
{
    class GameBoard<T>
    {
        private PlayingCards<T>[,] m_Board;
        private int m_BoardHeight;
        private int m_BoardLength;

        public GameBoard(int i_BoardHeight, int i_BoardLength, List<T> i_CardsValueArray)
        {
            m_BoardHeight = i_BoardHeight;
            m_BoardLength = i_BoardLength;
            createGameBoard(i_CardsValueArray);
        }

        public PlayingCards<T>[,] Board
        {
            get
            {
                return m_Board;
            }
        }

        public int BoardHeight
        {
            get
            {
                return m_BoardHeight;
            }
        }

        public int BoardLength
        {
            get
            {
                return m_BoardLength;
            }
        }

        private void createGameBoard(List<T> i_CardsValueArray)
        {
            Random random = new Random();
            List<T> matrixLetters = new List<T>();
            int gameBoardSize = m_BoardHeight * m_BoardLength;
            PlayingCards<T>[,] gameBoard = new PlayingCards<T>[m_BoardHeight, m_BoardLength];
            List<T> cardValueArrayCopyToMakeChanges = new List<T>(i_CardsValueArray);
            int lettersIndex = 0;

            if (gameBoardSize % 2 != 0)
            {
                throw new ArgumentException("The Board is not divisible by 2");
            }

            for (int i = 0; i < gameBoardSize / 2; ++i)
            {
                int randomLetterPoolIndex = random.Next(cardValueArrayCopyToMakeChanges.Count);
                T letterForPair = cardValueArrayCopyToMakeChanges[randomLetterPoolIndex];
                matrixLetters.Add(letterForPair);
                matrixLetters.Add(letterForPair);
                cardValueArrayCopyToMakeChanges.RemoveAt(randomLetterPoolIndex);
            }

            shuffleLetters(matrixLetters);
            for (int row = 0; row < m_BoardHeight; ++row)
            {
                for (int col = 0; col < m_BoardLength; ++col)
                {
                    gameBoard[row, col] = new PlayingCards<T>(matrixLetters[lettersIndex]);
                    ++lettersIndex;
                }
            }

            m_Board = gameBoard;
        }

        private void shuffleLetters(List<T> i_Values)
        {
            Random random = new Random();
            int remainingValues = i_Values.Count;

            while (remainingValues > 1)
            {
                remainingValues--;
                int swapIndex = random.Next(remainingValues + 1);
                T tempValue = i_Values[swapIndex];
                i_Values[swapIndex] = i_Values[remainingValues];
                i_Values[remainingValues] = tempValue;
            }
        }

        public void RestartBoard(int i_BoardHeight, int i_BoardLenght, List<T> i_CardsValueArray)
        {
            m_BoardHeight = i_BoardHeight;
            m_BoardLength = i_BoardLenght;
            createGameBoard(i_CardsValueArray);
        }
    }
}
