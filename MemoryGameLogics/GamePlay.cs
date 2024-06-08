using System;
using System.Collections.Generic;
using System.Linq;

namespace MemoryGameLogics
{
    public class GamePlay<T>
    {
        private GameBoard<T> m_GameBoard;
        private List<Player<T>> m_PlayersArray;
        private int m_PlayerTurn;
        private readonly List<T> m_CardsValueArray;
        private static bool s_IsFirstChoice = true;
        private int m_NumberOfVisibleCards;

        public GamePlay(List<string> i_PlayersNamesList, int i_BoardHeight, int i_BoardLenght, List<T> i_CardsValueArray)
        {
            const bool v_IsComputer = true;
            m_PlayersArray = initPlayerArray(i_PlayersNamesList);

            if (m_PlayersArray.Count <= 1)
            {
                m_PlayersArray.Add(new Player<T>(v_IsComputer));
            }
            m_NumberOfVisibleCards = 0;
            m_CardsValueArray = i_CardsValueArray;
            m_PlayerTurn = 0;
            m_GameBoard = new GameBoard<T>(i_BoardHeight, i_BoardLenght, m_CardsValueArray);
        }

        private List<Player<T>> initPlayerArray(List<string> i_PlayersNamesList)
        {
            const bool v_IsComputer = true;
            List<Player<T>> playerArray = new List<Player<T>>();

            for (int i = 0; i < i_PlayersNamesList.Count; ++i)
            {
                playerArray.Add(new Player<T>(!v_IsComputer, i_PlayersNamesList[i]));
            }

            return playerArray;
        }

        public PlayingCards<T>[,] GameBoard
        {
            get
            {
                return m_GameBoard.Board;
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
            return m_NumberOfVisibleCards >= (m_GameBoard.BoardHeight * m_GameBoard.BoardLength);
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

        public void CellChosenByPlayer(int i_RowIndex, int i_ColIndex)
        {
            int currentPlayerIndex = this.PlayerTurn;

            if (s_IsFirstChoice)
            {
                m_PlayersArray[currentPlayerIndex].FirstChosenValue = m_GameBoard.Board[i_RowIndex, i_ColIndex];
                m_PlayersArray[currentPlayerIndex].FirstChosenValue.VisibilityOption = eVisibleOptions.TemporaryVisible;
            }
            else
            {
                m_PlayersArray[currentPlayerIndex].SecondChosenValue = m_GameBoard.Board[i_RowIndex, i_ColIndex];
                m_PlayersArray[currentPlayerIndex].SecondChosenValue.VisibilityOption = eVisibleOptions.TemporaryVisible;
            }

            s_IsFirstChoice = !s_IsFirstChoice;
        }

        public bool IsCorrectGuess()
        {
            bool IsCorrectGuess;
            int currentPlayerIndex = this.PlayerTurn;
            PlayingCards<T> firstChosenValue = m_PlayersArray[currentPlayerIndex].FirstChosenValue;
            PlayingCards<T> secondChosenValue = m_PlayersArray[currentPlayerIndex].SecondChosenValue;

            if (firstChosenValue.CardValue.Equals(secondChosenValue.CardValue))
            {
                IsCorrectGuess = true;
                m_PlayersArray[currentPlayerIndex].NumOfCorrectAnswers++;
                firstChosenValue.VisibilityOption = eVisibleOptions.Visible;
                secondChosenValue.VisibilityOption = eVisibleOptions.Visible;
                m_NumberOfVisibleCards += 2;
            }
            else
            {
                IsCorrectGuess = false;
            }

            return IsCorrectGuess;
        }

        public void HideVisibilityOfTemporaryTypeAndAdvanceTurn()
        {
            m_PlayersArray[PlayerTurn].FirstChosenValue.VisibilityOption = eVisibleOptions.NotVisible;
            m_PlayersArray[PlayerTurn].SecondChosenValue.VisibilityOption = eVisibleOptions.NotVisible;
            this.PlayerTurn++;
        }

        public void ComputerChoice()
        {
            Random random = new Random();
            (int, int) randomRowAndColTuppel;
            int randomRowIndex;
            int randomColIndex;

            List<(int, int)> invisibleCards = new List<(int, int)>();

            for (int row = 0; row < m_GameBoard.BoardHeight; ++row)
            {
                for (int col = 0; col < m_GameBoard.BoardLength; ++col)
                {
                    if (m_GameBoard.Board[row, col].VisibilityOption == eVisibleOptions.NotVisible)
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
            m_NumberOfVisibleCards = 0;
            m_PlayerTurn = 0;

            foreach (var player in m_PlayersArray)
            {
                player.NumOfCorrectAnswers = 0;
            }
            m_GameBoard.RestartBoard(i_BoardHeight, i_BoardLenght, m_CardsValueArray);
        }
    }
}
