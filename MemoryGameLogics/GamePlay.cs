using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGameLogics
{
    internal class GamePlay
    {
        // todo:
        // 1. try to make the whole class templates
        // 2. check correctness
        // 3. think about integration with UI

        private PlayingCards<char>[,] m_GameBoard;
        private int m_BoardHeight;
        private int m_BoardLength;
        private Player m_Player1;
        private Player m_Player2;
        private const string k_letterPool = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private GamePlay(Player i_Player1, Player i_Player2, int i_BoardHeight, int i_BoardLenght)
        {
            m_Player1 = i_Player1;
            m_Player2 = i_Player2;
            m_BoardHeight = i_BoardHeight;
            m_BoardLength = i_BoardLenght;
            createGameBoard();
        }

        private GamePlay(Player i_Player1, int i_BoardHeight, int i_BoardLenght)
        {
            const bool v_IsComputer = true;
            m_Player1 = i_Player1;
            m_Player2 = new Player(v_IsComputer);
            m_BoardHeight = i_BoardHeight;
            m_BoardLength = i_BoardLenght;
            createGameBoard();
        }

        private PlayingCards<char>[,] GameBoard
        {
            get
            {
                return m_GameBoard;
            }
        }

        private void createGameBoard()
        {
            Random random = new Random();
            List<char> matrixLetters = new List<char>();
            int gameBoardSize = m_BoardHeight * m_BoardLength;
            PlayingCards<char>[,] gameBoard = new PlayingCards<char>[m_BoardHeight, m_BoardLength];
            int lettersIndex = 0;
            List<char> letterPoolList = k_letterPool.ToList();

            for (int i = 0; i < gameBoardSize / 2; ++i)
            {
                int randomLetterPoolIndex = random.Next(letterPoolList.Count); // need to make sure it works
                char letterForPair = letterPoolList[randomLetterPoolIndex];
                matrixLetters.Add(letterForPair);
                matrixLetters.Add(letterForPair);
                letterPoolList.RemoveAt(randomLetterPoolIndex);
            }

            shuffleLetters(matrixLetters);
            for (int row = 0; row < m_BoardHeight; ++row)
            {
                for (int col = 0; col < m_BoardLength; ++col)
                {
                    gameBoard[row, col].CardValue = matrixLetters[lettersIndex];
                    gameBoard[row, col].IsVisible = false;
                    ++lettersIndex;
                }
            }

            m_GameBoard = gameBoard;
        }

        private void shuffleLetters(List<char> i_Letters)
        {
            Random random = new Random();
            int remainingLetters = i_Letters.Count;

            while (remainingLetters > 1)
            {
                remainingLetters--;
                int swapIndex = random.Next(remainingLetters + 1);
                char tempLetter = i_Letters[swapIndex];
                i_Letters[swapIndex] = i_Letters[remainingLetters];
                i_Letters[remainingLetters] = tempLetter;
            }
        }

        private char firstCellChosenByPlayer(bool i_IsPlayer1, int i_RowIndex, int i_ColIndex)
        {
            if (i_IsPlayer1)
            {
                m_Player1.FirstChosenLetter = m_GameBoard[i_RowIndex, i_ColIndex];
                m_Player1.FirstChosenLetter.RowNumber = i_RowIndex;
                m_Player1.FirstChosenLetter.ColNumber = i_ColIndex;
            }
            else
            {
                m_Player2.FirstChosenLetter = m_GameBoard[i_RowIndex, i_ColIndex];
                m_Player2.FirstChosenLetter.RowNumber = i_RowIndex;
                m_Player2.FirstChosenLetter.ColNumber = i_ColIndex;
            }

            return m_GameBoard[i_RowIndex, i_ColIndex].CardValue;
        }

        private bool secondCellChosenByPlayer(bool i_IsPlayer1, int i_RowIndex, int i_ColIndex)
        {
            // todo - make sure to make the first choice visible if correct
            bool isCorrectAnswer;
            char secondChosenLetter = m_GameBoard[i_RowIndex, i_ColIndex].CardValue;

            if (i_IsPlayer1)
            {
                if (m_Player1.FirstChosenLetter.CardValue == secondChosenLetter)
                {
                    isCorrectAnswer = true;
                    m_Player1.NumOfCorrectAnswers++;
                    m_GameBoard[m_Player1.FirstChosenLetter.RowNumber, m_Player1.FirstChosenLetter.ColNumber].IsVisible = true;
                    m_GameBoard[i_RowIndex, i_ColIndex].IsVisible = true;
                }
                else 
                {
                    isCorrectAnswer = false;
                }
            }
            else
            {
                if (m_Player2.FirstChosenLetter.CardValue == secondChosenLetter)
                {
                    isCorrectAnswer = true;
                    m_Player2.NumOfCorrectAnswers++;
                    m_GameBoard[m_Player2.FirstChosenLetter.RowNumber, m_Player2.FirstChosenLetter.ColNumber].IsVisible = true;
                    m_GameBoard[i_RowIndex, i_ColIndex].IsVisible = true;
                }
                else
                {
                    isCorrectAnswer = false;
                }
            }

            return isCorrectAnswer;
        }

        private void computerChoice(out int o_RowIndex, out int o_ColIndex)
        {
            Random random = new Random();

            o_RowIndex = random.Next(m_BoardHeight);
            o_ColIndex = random.Next(m_BoardLength);
        }
    }
}
