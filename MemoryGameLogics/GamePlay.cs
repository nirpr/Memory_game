using System;
using System.Collections.Generic;
using System.Linq;

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
        private static bool s_IsFirstChoice = true;
        private int m_NumberOfVisibleCards;

        public GamePlay(List<string> i_PlayersNamesList, int i_BoardHeight, int i_BoardLenght)
        {
            const bool v_IsComputer = true;
            m_PlayersArray = initPlayerArray(i_PlayersNamesList);

            if (m_PlayersArray.Count <= 1)
            {
                m_PlayersArray.Add(new Player(v_IsComputer));
            }
            m_BoardHeight = i_BoardHeight;
            m_BoardLength = i_BoardLenght;
            m_NumberOfVisibleCards = 0;
            m_PlayerTurn = 0;
            createGameBoard();
        }

        private List<Player> initPlayerArray(List<string> i_PlayersNamesList)
        {
            const bool v_IsComputer = true;
            List<Player> playerArray = new List<Player>();

            for (int i = 0; i < i_PlayersNamesList.Count; ++i)
            {
                playerArray.Add(new Player(!v_IsComputer, i_PlayersNamesList[i]));
            }

            return playerArray;
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
                if (m_PlayerTurn < m_PlayersArray.Count - 1)
                {
                    m_PlayerTurn = value;
                }
                else
                {
                    m_PlayerTurn = 0;
                }
            }

        }

        public int NumberOfVisibleCards
        {
            get
            {
                return m_NumberOfVisibleCards;
            }
            private set
            {
                m_NumberOfVisibleCards = value;
            }
        }

        public bool IsComputerTurn()
        {
            return m_PlayersArray[this.PlayerTurn].IsComputer;
        }

        public string CurrentPlayerName()
        {
            return m_PlayersArray[this.PlayerTurn].PlayerName;
        }

        public string GetNameByIndex(int i_NameIndexInPlayersArray)
        {
            return m_PlayersArray[i_NameIndexInPlayersArray].PlayerName;
        }

        public bool IsGameOver()
        {
            return this.NumberOfVisibleCards >= (m_BoardHeight * m_BoardLength);
        }

        public List<int> ArrayOfPlayersScores()
        {
            List<int> playersScores = new List<int>();

            foreach (var player in m_PlayersArray)
            {
                playersScores.Add(player.NumOfCorrectAnswers);
            }

            return playersScores;
        }

        private void createGameBoard()
        {
            Random random = new Random();
            List<char> matrixLetters = new List<char>();
            int gameBoardSize = m_BoardHeight * m_BoardLength;
            PlayingCards<char>[,] gameBoard = new PlayingCards<char>[m_BoardHeight, m_BoardLength];
            int lettersIndex = 0;
            List<char> letterPoolList = k_letterPool.ToList();

            if (gameBoardSize % 2 != 0)
            {
                throw new ArgumentException("The Board is not divisible by 2");
            }

            for (int i = 0; i < gameBoardSize / 2; ++i)
            {
                int randomLetterPoolIndex = random.Next(letterPoolList.Count);
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
                    gameBoard[row, col] = new PlayingCards<char>(matrixLetters[lettersIndex]);
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

        public void CellChosenByPlayer(int i_RowIndex, int i_ColIndex)
        {
            int currentPlayerIndex = this.PlayerTurn;

            if (s_IsFirstChoice)
            {
                m_PlayersArray[currentPlayerIndex].FirstChosenLetter = m_GameBoard[i_RowIndex, i_ColIndex];
                m_PlayersArray[currentPlayerIndex].FirstChosenLetter.VisibilityOption = eVisibleOptions.TemporaryVisible;
            }
            else
            {
                m_PlayersArray[currentPlayerIndex].SecondChosenLetter = m_GameBoard[i_RowIndex, i_ColIndex];
                m_PlayersArray[currentPlayerIndex].SecondChosenLetter.VisibilityOption = eVisibleOptions.TemporaryVisible;
            }

            s_IsFirstChoice = !s_IsFirstChoice;
        }

        public bool IsCorrectGuess()
        {
            bool IsCorrectGuess;
            int currentPlayerIndex = this.PlayerTurn;
            PlayingCards<char> firstChosenLetter = m_PlayersArray[currentPlayerIndex].FirstChosenLetter;
            PlayingCards<char> secondChosenLetter = m_PlayersArray[currentPlayerIndex].SecondChosenLetter;

            if (firstChosenLetter.CardValue == secondChosenLetter.CardValue)
            {
                IsCorrectGuess = true;
                m_PlayersArray[currentPlayerIndex].NumOfCorrectAnswers++;
                firstChosenLetter.VisibilityOption = eVisibleOptions.Visible;
                secondChosenLetter.VisibilityOption = eVisibleOptions.Visible;
                this.NumberOfVisibleCards += 2;
            }
            else
            {
                IsCorrectGuess = false;
            }

            return IsCorrectGuess;
        }

        public void HideVisibilityOfTemporaryTypeAndAdvanceTurn()
        {
            m_PlayersArray[PlayerTurn].FirstChosenLetter.VisibilityOption = eVisibleOptions.NotVisible;
            m_PlayersArray[PlayerTurn].SecondChosenLetter.VisibilityOption = eVisibleOptions.NotVisible;
            this.PlayerTurn++;
        }

        public void ComputerChoice()
        {
            Random random = new Random();
            (int, int) randomRowAndColTuppel;
            int randomRowIndex;
            int randomColIndex;

            List<(int, int)> invisibleCards = new List<(int, int)>();

            for (int row = 0; row < m_BoardHeight; ++row)
            {
                for (int col = 0; col < m_BoardLength; ++col)
                {
                    if (m_GameBoard[row, col].VisibilityOption == eVisibleOptions.NotVisible)
                    {
                        invisibleCards.Add((row, col));
                    }
                }
            }

            randomRowAndColTuppel = invisibleCards[random.Next(invisibleCards.Count)];
            randomRowIndex = randomRowAndColTuppel.Item1;
            randomColIndex = randomRowAndColTuppel.Item2;
            CellChosenByPlayer(randomRowIndex, randomColIndex);
        }

        public void RestartGame(int i_BoardHeight, int i_BoardLenght)
        {
            m_BoardHeight = i_BoardHeight;
            m_BoardLength = i_BoardLenght;
            m_NumberOfVisibleCards = 0;
            m_PlayerTurn = 0;

            foreach (var player in m_PlayersArray)
            {
                player.NumOfCorrectAnswers = 0;
            }
            createGameBoard();
        }
    }
}
