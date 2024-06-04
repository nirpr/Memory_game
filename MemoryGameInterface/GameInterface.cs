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

                playerMove();

                // if GameStatus = { END or Process }
                // TODO: if (m_GamePlay.GameOver)   // - END: Show GameOver -> Announce Winner -> Ask for restart game
                {
                    GameConsoleUtils.AnnounceGameOver(m_GamePlay);
                    bool playRematch = askUserForRestartGame();
                    if (playRematch)
                    {
                        int boardHeight, boardWidth;
                        askUserForBoardSize(out boardHeight, out boardWidth);
                        // TODO: make this method: m_GamePlay.rematch(boardHeight, boardWidth);
                    }
                    else
                    {
                        gameIsStillPlaying = false;
                    }
                }
                // - Process: skip for next round.
            }
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

        private void playerMove()
        {
            // TODO: if current player is computer
            // - make random guess by GameLogic

            int rowIndex, colIndex;
            selectGameCell(out rowIndex, out colIndex);
            m_GamePlay.firstCellChosenByPlayer(rowIndex, colIndex);
            GameConsoleUtils.ShowGameBoard(m_GamePlay);

            selectGameCell(out rowIndex, out colIndex);
            m_GamePlay.firstCellChosenByPlayer(rowIndex, colIndex);
            GameConsoleUtils.ShowGameBoard(m_GamePlay);
        }

        private void askUserForBoardSize(out int o_BoardHeight, out int o_BoardWidth)
        {
            const bool k_WaitingTillValidInput = true;
            Console.WriteLine($"Choose the size of the game board, The total number of squares on the board should be even.");


            while (k_WaitingTillValidInput)
            {
                string heightMsg = $"Please enter the board height in range [{k_BoardHeightMinimumSize}-{k_BoardHeightMaximumSize}]";
                o_BoardHeight = GameConsoleUtils.askUserForIntBetweenValues(heightMsg, k_BoardHeightMinimumSize, k_BoardHeightMaximumSize);

                string widthMsg = $"Please enter the board height in range [{k_BoardHeightMinimumSize}-{k_BoardHeightMaximumSize}]";
                o_BoardWidth = GameConsoleUtils.askUserForIntBetweenValues(widthMsg, k_BoardWidthMinimumSize, k_BoardWidthMaximumSize);
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

        private bool askUserForRestartGame()
        {
            const bool k_WaitingTillValidInput = true;

            while (k_WaitingTillValidInput)
            {
                string userInput = GameConsoleUtils.askForUserInput("Do you want to play again? [Y/N]");
                bool isInputValid = GameConsoleUtils.validateYesOrNoInput(userInput);

                if (isInputValid)
                {
                    return Converter.ConvertYesOrNoToBool(userInput);
                }
                else
                {
                    Console.WriteLine("Invalid response, Try again !");
                }
            }
        }

        private void setPlayers(List<string> i_ListOfPlayerNames)
        {
            bool isGameAgaintsComputer = false;

            string playerOneName = GameConsoleUtils.askForUserInput("Hello, What is the name of the first player ? ");
            i_ListOfPlayerNames.Add(playerOneName);

            isGameAgaintsComputer = GameConsoleUtils.askUserForYesOrNoQuestion("Do you want to play against the computer? [Y/N]");

            if (isGameAgaintsComputer)
            {
                string playerTwoName = GameConsoleUtils.askForUserInput("What is the name of the second player ? ");
                i_ListOfPlayerNames.Add(playerTwoName);
            }
        }

        private void selectGameCell(out int o_rowIndex, out int o_colIndex)
        {
            int rowIndex, colIndex;
            const bool k_WaitingTillValidSelection = true;
            while(k_WaitingTillValidSelection)
            {
               
                string userInput = GameConsoleUtils.askForUserInput("Please enter game cell for your move: ");
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
