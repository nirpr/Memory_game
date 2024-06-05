using Ex02.ConsoleUtils;
using MemoryGameLogics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGameInterface
{
    internal class GameConsoleUtils
    {
        private const char k_Space = ' ';
        private const string k_ColumnSeparator = "|";
        private const int k_NumberOfSpacesBeforeStartLine = 3;

        private static void printBoard<T>(PlayingCards<T>[,] i_Board)
        {
            const int k_IndexForRowsInBoard = 0, k_IndexForColsInBoard = 1;
            int numOfRowsIncludeSeparations = i_Board.GetLength(k_IndexForRowsInBoard) * 2 + 1;

            printHeadLineOfIndexes(i_Board.GetLength(k_IndexForColsInBoard));
            for (int rowIndex = 0; rowIndex < numOfRowsIncludeSeparations; rowIndex++)
            {
                if (rowIndex % 2 == 0)
                {
                    printRowForSeparation(i_Board.GetLength(k_IndexForColsInBoard));
                }
                else
                {
                    Console.Write($" {(rowIndex / 2)+1} {k_ColumnSeparator}");
                    printRowOfCards(i_Board, rowIndex / 2);
                }
            }
        }

        public static void ShowGameBoard(GamePlay i_GamePlay)
        {
            PlayingCards<char>[,] gameBoard = i_GamePlay.GameBoard;

            Screen.Clear();
            GameConsoleUtils.printBoard(gameBoard);
        }

        public static void AnnounceAboutCurrentPlayerTurn(GamePlay i_GamePlay)
        {
            int currentPlayerId = i_GamePlay.PlayerTurn;
            
            Console.WriteLine($"It's {i_GamePlay.CurrentPlayerName()}'s turn now (Player{currentPlayerId+1})");
        }

        public static void GameStartingAnnouncement()
        {
            Console.WriteLine("Everything is ready for start the game, let's start!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }

        public static void AnnounceGameOver(GamePlay i_GamePlay)
        {
            PrintScoreBoard(i_GamePlay);
            AnnounceAboutWinner(i_GamePlay);
        }

        public static void PrintScoreBoard(GamePlay i_GamePlay)
        {
            StringBuilder leaderBoard = new StringBuilder();
            List<int> scoreBoardArray = i_GamePlay.ArrayOfPlayersScores();

            leaderBoard.AppendLine("--------- Score Board ---------");
            for(int i=0; i<scoreBoardArray.Count; i++)
            {
                leaderBoard.AppendLine($"Player{i+1}: {i_GamePlay.GetNameByIndex(i)}, Score: {scoreBoardArray[i]}");
            }

            Console.WriteLine(leaderBoard);
        }

        public static void AnnounceAboutWinner(GamePlay i_GamePlay)
        {
            List<int> scoreBoardArray = i_GamePlay.ArrayOfPlayersScores();
            int highestScoreInGame = scoreBoardArray.Max();
            List<string> Winners = new List<string>();

            for(int i=0;i< scoreBoardArray.Count; i++) 
            {
                if (scoreBoardArray[i] == highestScoreInGame)
                {
                    Winners.Add(i_GamePlay.GetNameByIndex(i));
                }
            }

            if(Winners.Count >= 2)
            {
                Console.Write("There is a tie between");
                foreach (string name in Winners)
                {
                    Console.Write($", {name}");
                }
                Console.WriteLine(".");
            }
            else
            {
                Console.WriteLine($"The winner of this round of the game is {Winners[0]} with Score of {highestScoreInGame}!");
            }
        }

        private static void printHeadLineOfIndexes(int i_Size)
        {
            StringBuilder indexesLineString = new StringBuilder();

            indexesLineString.Append(k_Space, k_NumberOfSpacesBeforeStartLine);
            indexesLineString.Append(k_ColumnSeparator);
            for (int colIndex = 0; colIndex < i_Size; colIndex++)
            {
                indexesLineString.Append($" {Converter.ConvertNumericIndexToLetterIndex(colIndex).ToString()} {k_ColumnSeparator}");
            }

            Console.WriteLine(indexesLineString);
        }

        private static void printRowOfCards<T>(PlayingCards<T>[,] i_GameBoard, int i_RowIndex)
        {
            T playCard;
            const int k_IndexForColsInBoard = 1;
            const int k_SpacesForHiddenCard = 3;
            StringBuilder rowOfCards = new StringBuilder();

            for (int colIndex = 0; colIndex < i_GameBoard.GetLength(k_IndexForColsInBoard); colIndex++)
            {
                PlayingCards<T> playingCard = i_GameBoard[i_RowIndex, colIndex];
                if (playingCard.VisibilityOption == eVisibleOptions.Visible || playingCard.VisibilityOption == eVisibleOptions.TemporaryVisible)
                {
                    playCard = i_GameBoard[i_RowIndex, colIndex].CardValue;
                    rowOfCards.Append($" {playCard.ToString()} {k_ColumnSeparator}");
                }
                else
                {
                    rowOfCards.Append(k_Space, k_SpacesForHiddenCard);
                    rowOfCards.Append(k_ColumnSeparator);
                }
            }

            Console.WriteLine(rowOfCards);
        }

        private static void printRowForSeparation(int i_Width)
        {
            const int k_NumberOfBottonSepratorsForEachCard = 4;
            
            StringBuilder rowOfSepration = new StringBuilder();

            rowOfSepration.Append(k_Space, k_NumberOfSpacesBeforeStartLine);
            for (int colIndex = 0; colIndex < i_Width; colIndex++)
            {
                rowOfSepration.Append('=', k_NumberOfBottonSepratorsForEachCard);
            }

            Console.WriteLine(rowOfSepration);
        }

        public static string askForUserInput(string i_MessageForUser)
        {
            Console.WriteLine(i_MessageForUser);

            return Console.ReadLine();
        }

        private static bool validateYesOrNoInput(string i_UserInput)
        {
            string userInputInUpperCase = i_UserInput.ToUpper();

            return userInputInUpperCase == "Y" || userInputInUpperCase == "N";
        }

        public static bool AskUserForYesOrNoQuestion(string i_questionForUser)
        {
            const bool k_WaitingTillValidInput = true;

            while (k_WaitingTillValidInput)
            {
                string userInput = askForUserInput(i_questionForUser);
                bool isInputValid = validateYesOrNoInput(userInput);
                if (isInputValid)
                {
                    return Converter.ConvertYesOrNoToBool(userInput);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again!");
                }
            }
        }

        public static int AskUserForIntBetweenValues(string i_questionForUser, int i_MinValue, int i_MaxValue)
        {
            const bool k_WaitingTillValidInput = true;

            while (k_WaitingTillValidInput)
            {
                int userInputInt;
                string userInput = askForUserInput(i_questionForUser);
                bool isInputValid = int.TryParse(userInput, out userInputInt);
                bool isInputInRange = i_MinValue <= userInputInt && userInputInt <= i_MaxValue;

                if (isInputValid && isInputInRange)
                {
                    return userInputInt;
                }
                else
                {
                    Console.WriteLine($"Invalid input, the value should be in range [{i_MinValue}-{i_MaxValue}]. Please try again!");
                }
            }
        }
    }
}
