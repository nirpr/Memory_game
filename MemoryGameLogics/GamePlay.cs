using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGameLogics
{
    public class GamePlay
    {
        private PlayingCards<char>[,] m_GameBoard;
        private int m_BoardHeight;
        private int m_BoardLength;
        private List<Player> m_PlayersArray;
        private int m_PlayerTurn;
        private const string k_letterPool = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public GamePlay(List<string> i_PlayersNamesList, int i_BoardHeight, int i_BoardLenght)
        {
            const bool v_IsComputer = true;
            m_PlayersArray = InitPlayerList(i_PlayersNamesList);

            if (m_PlayersArray.Count <= 1)
            {
                m_PlayersArray.Add(new Player(v_IsComputer));
            }
            m_BoardHeight = i_BoardHeight;
            m_BoardLength = i_BoardLenght;
            m_PlayerTurn = 0;
            createGameBoard();
        }

        private List<Player> InitPlayerList(List<string> i_PlayersNamesList)
        {
            const bool v_IsComputer = true;
            List<Player> playerList = new List<Player>();

            for (int i = 0; i < i_PlayersNamesList.Count; ++i)
            {
                playerList.Add(new Player(!v_IsComputer, i_PlayersNamesList[i]));
            }

            return playerList;
        }

        public PlayingCards<char>[,] GameBoard
        {
            get
            {
                return m_GameBoard;
            }
        }

        public int PlayerTurn
        {
            get
            {
                return m_PlayerTurn;
            }
            private set
            {
                if (m_PlayerTurn < m_PlayersArray.Count)
                {
                    m_PlayerTurn += value;
                }
                else
                {
                    m_PlayerTurn = 1;
                }
            }

        }

        public bool isComputerTurn()
        {
            return m_PlayersArray[this.PlayerTurn].IsComputer;
        }

        private void createGameBoard()
        {
            Random random = new Random();
            List<char> matrixLetters = new List<char>();
            int gameBoardSize = m_BoardHeight * m_BoardLength;
            PlayingCards<char>[,] gameBoard = new PlayingCards<char>[m_BoardHeight, m_BoardLength];
            int lettersIndex = 0;
            List<char> letterPoolList = k_letterPool.ToList();
            const bool v_MakeVisible = true;

            if (gameBoardSize % 2 != 0)
            {
                throw new ArgumentException("The Board is not divisible by 2");
            }

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
                    gameBoard[row, col] = new PlayingCards<char>();
                    gameBoard[row, col].CardValue = matrixLetters[lettersIndex];
                    gameBoard[row, col].IsVisible = !v_MakeVisible;
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

        public char firstCellChosenByPlayer(int i_RowIndex, int i_ColIndex)
        {
            int currentPlayerIndex = this.PlayerTurn;
            m_PlayersArray[currentPlayerIndex].FirstChosenLetter = m_GameBoard[i_RowIndex, i_ColIndex];
            m_PlayersArray[currentPlayerIndex].FirstChosenLetter.RowNumber = i_RowIndex;
            m_PlayersArray[currentPlayerIndex].FirstChosenLetter.ColNumber = i_ColIndex;

            return m_GameBoard[i_RowIndex, i_ColIndex].CardValue; // maybe change to void, check with omer
        }

        public bool secondCellChosenByPlayer(int i_RowIndex, int i_ColIndex)
        {
            const bool v_MakeVisible = true;
            bool isCorrectAnswer;
            int currentPlayerIndex = this.PlayerTurn;
            PlayingCards<char> firstChosenLetter = m_PlayersArray[currentPlayerIndex].FirstChosenLetter;
            PlayingCards<char> secondChosenLetter = m_GameBoard[i_RowIndex, i_ColIndex];
            

            if (firstChosenLetter.CardValue == secondChosenLetter.CardValue)
            {
                isCorrectAnswer = true;
                m_PlayersArray[currentPlayerIndex].NumOfCorrectAnswers++;
                firstChosenLetter.IsVisible = v_MakeVisible;
                secondChosenLetter.IsVisible = v_MakeVisible;
            }
            else
            {
                isCorrectAnswer = false;
                this.PlayerTurn++;
            }

            return isCorrectAnswer;
        }

        public void computerChoice(out int o_RowIndex, out int o_ColIndex)
        {
            Random random = new Random();

            o_RowIndex = random.Next(m_BoardHeight);
            o_ColIndex = random.Next(m_BoardLength);
        }
    }
}
