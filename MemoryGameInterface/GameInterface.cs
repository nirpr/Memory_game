using MemoryGameLogics;
using System;
using System.Collections.Generic;

namespace MemoryGameInterface
{
    public class GameInterface
    {
        const int k_BoardHeightMinimumSize = 4, k_BoardHeightMaximumSize = 6;
        const int k_BoardWidthMinimumSize = 4, k_BoardWidthMaximumSize = 6;

        GamePlay m_GamePlay = null;

        public void StartGame()
        {
            bool gameIsStillPlaying = true;

            m_GamePlay = gameInitilize();

            GameConsoleUtils.GameStartingAnnouncement();

            GameConsoleUtils.ShowGameBoard(m_GamePlay);

            while (gameIsStillPlaying)
            {
                GameConsoleUtils.AnnounceAboutCurrentPlayerTurn(m_GamePlay);

                playerMove(out gameIsStillPlaying);

                if(m_GamePlay.IsGameOver())
                {
                    GameConsoleUtils.AnnounceGameOver(m_GamePlay);
                    bool playRematch = GameConsoleUtils.AskUserForYesOrNoQuestion("Do you want to play again? [Y/N]");
                    if (playRematch)
                    {
                        int boardHeight, boardWidth;
                        askUserForBoardSize(out boardHeight, out boardWidth);
                        m_GamePlay.RestartGame(boardHeight, boardWidth);
                    }
                    else
                    {
                        gameIsStillPlaying = false;
                    }
                }
            }

            Console.WriteLine("Game is over, good bye !");
        }

        private GamePlay gameInitilize()
        {
            List<string> listOfPlayerNames = new List<string>();
            int boardHeight, boardWidth;

            setPlayers(listOfPlayerNames);
            askUserForBoardSize(out boardHeight, out boardWidth);

            GamePlay gamePlay = new GamePlay(listOfPlayerNames, 4, 4);

            return gamePlay;
        }

        private void playerMove(out bool o_GameIsStillPlaying)
        {
            const int k_NumberOfGuesses = 2;
            const int k_FirstGuess = 1;
            const int k_TwoSecondsInMiliseconds = 2000;
            o_GameIsStillPlaying = false;

            if (m_GamePlay.IsComputerTurn())
            {
                o_GameIsStillPlaying = false;
                for (int guess = 1; guess <= k_NumberOfGuesses; guess++)
                {
                    GameConsoleUtils.ShowGameBoard(m_GamePlay);
                    if (guess == k_FirstGuess)
                    {
                        System.Threading.Thread.Sleep(k_TwoSecondsInMiliseconds);
                    }
                }
            }
            else
            {
                int rowIndex, colIndex;
                bool playerWantQuitGame;

                for (int guess = 1; guess <= k_NumberOfGuesses; guess++)
                {
                    selectGameCell(out rowIndex, out colIndex, out playerWantQuitGame);
                    if (playerWantQuitGame && GameConsoleUtils.AskUserForYesOrNoQuestion("Are you sure you want Quit the game ? [Y/N]"))
                    {
                        o_GameIsStillPlaying = false;
                    }
                    else
                    {
                        o_GameIsStillPlaying = true;
                    }
                    m_GamePlay.CellChosenByPlayer(rowIndex, colIndex);
                    GameConsoleUtils.ShowGameBoard(m_GamePlay);
                }
            }

            if (m_GamePlay.IsCorrectGuess() == false)
            {
                System.Threading.Thread.Sleep(k_TwoSecondsInMiliseconds);
                GameConsoleUtils.ShowGameBoard(m_GamePlay);
            }
        }

        private void askUserForBoardSize(out int o_BoardHeight, out int o_BoardWidth)
        {
            const bool v_WaitingTillValidInput = true;
            Console.WriteLine($"Choose the size of the game board, The total number of squares on the board should be even.");

            while (v_WaitingTillValidInput)
            {
                string heightMsg = $"Please enter the board height in range [{k_BoardHeightMinimumSize}-{k_BoardHeightMaximumSize}]";
                o_BoardHeight = GameConsoleUtils.AskUserForIntBetweenValues(heightMsg, k_BoardHeightMinimumSize, k_BoardHeightMaximumSize);

                string widthMsg = $"Please enter the board height in range [{k_BoardHeightMinimumSize}-{k_BoardHeightMaximumSize}]";
                o_BoardWidth = GameConsoleUtils.AskUserForIntBetweenValues(widthMsg, k_BoardWidthMinimumSize, k_BoardWidthMaximumSize);
                if((o_BoardHeight * o_BoardWidth)%2 == 0)
                {
                    Console.WriteLine($"The gameboard selected to be in size of {o_BoardHeight}x{o_BoardWidth}");
                    break;
                }
                else
                {
                    Console.WriteLine($"Invalid size, The total number of squares on the board should be even. Try again !");
                }
            }
        }

        private void setPlayers(List<string> i_ListOfPlayerNames)
        {
            string playerOneName = GameConsoleUtils.askForUserInput("Hello, What is the name of the first player ?");
            i_ListOfPlayerNames.Add(playerOneName);

            bool isGameAgaintsComputer = GameConsoleUtils.AskUserForYesOrNoQuestion("Do you want to play against the computer? [Y/N]");

            if (isGameAgaintsComputer == false)
            {
                string playerTwoName = GameConsoleUtils.askForUserInput("What is the name of the second player ?");
                i_ListOfPlayerNames.Add(playerTwoName);
            }
        }

        private void selectGameCell(out int o_rowIndex, out int o_colIndex, out bool o_PlayerWantQuit)
        {
            int rowIndex, colIndex;
            const bool k_WaitingTillValidSelection = true;
            const int k_NotRelevant_Value = -1;

            while(k_WaitingTillValidSelection)
            {
                string userInput = GameConsoleUtils.askForUserInput("Please enter game cell for your move:");

                if(userInput == "Q")
                {
                    o_PlayerWantQuit = true;
                    o_rowIndex = k_NotRelevant_Value;
                    o_colIndex = k_NotRelevant_Value;
                    return;
                }
                else
                {
                    o_PlayerWantQuit = false;
                }

                try
                {
                    (colIndex, rowIndex) = Converter.CellReferenceConverter.ConvertCellIndex(userInput);
                    PlayingCards<char> selectedPlayingCard = m_GamePlay.GameBoard[rowIndex, colIndex];
                    if(selectedPlayingCard.IsVisible)
                    {
                        Console.WriteLine("The selected location is already displayed You have reached Unable to select it, try again");
                    }
                    else
                    {
                        break;
                    }
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine($"{ e.Message}, Try again!");
                }
            }
            o_rowIndex = rowIndex;
            o_colIndex = colIndex;
        }
    }

}
