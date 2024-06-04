using Ex02.ConsoleUtils;
using MemoryGameLogics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGameInterface
{
    public class GameConsoleUtils
    {
        private const string k_Space = " ";
        private const string k_ColumnSeparator = "|";

        public static void PrintBoard<T>(PlayingCards<T>[,] i_Board)
        {
            int numOfRowsIncludeSeparations = i_Board.GetLength(0) * 2;

            printHeadLineOfIndexes(i_Board.GetLength(0));
            for (int rowIndex = 0; rowIndex < numOfRowsIncludeSeparations; rowIndex++)
            {
                if (rowIndex % 2 == 0)
                {
                    Console.Write($"{rowIndex / 2}{k_ColumnSeparator}");
                    printRowOfCards(ref i_Board, rowIndex / 2);
                }
                else
                {
                    Console.Write(k_Space);
                    printRowForSeparation(i_Board.GetLength(0));
                }
            }
        }

        public static void ShowGameBoard(GamePlay i_GamePlay)
        {
            PlayingCards<char>[,] gameBoard = i_GamePlay.GameBoard;

            Screen.Clear();
            GameConsoleUtils.PrintBoard(gameBoard);
        }

        public static void AnnounceAboutCurrentPlayerTurn(GamePlay i_GamePlay)
        {
            int currentPlayerId = i_GamePlay.PlayerTurn;

            if (currentPlayerId == 1)
            {
                Console.WriteLine("Player 1 turn!");
            }
            else
            {
                bool isPlayerTwoComputer = false; // TODO: make it change by player type.
                Console.WriteLine($"{(isPlayerTwoComputer ? "Computer" : "Player 2")}  turn!");
            }
        }

        public static void GameStartingAnnouncement()
        {
            Console.WriteLine("Everything is ready for start the game, let's start!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }

        public static void AnnounceGameOver(GamePlay i_GamePlay)
        {
            //const byte k_Player1Id = 1;
            //const byte k_Player2Id = 2;

            //switch (who win)
            //{
            //    case Player1Win:
            //        AnnounceAboutWinner(k_Player1Id);
            //        break;
            //    case Player2Win:
            //        AnnounceAboutWinner(k_Player2Id);
            //        break;
            //    case Draw:
            //        Console.WriteLine("It's a draw!");
            //        break;
            //}
        }

        public static void AnnounceAboutWinner(byte i_WinnerId)
        {
            Console.WriteLine($"The winner of this round of the game is player {i_WinnerId}!");
        }

        private static void printHeadLineOfIndexes(int i_Size)
        {
            StringBuilder indexesLineString = new StringBuilder();

            indexesLineString.Append(k_Space);
            for (int colIndex = 0; colIndex < i_Size; colIndex++)
            {
                indexesLineString.Append($"  {Converter.ConvertNumericIndexToLetterIndex(colIndex).ToString()}");
            }

            Console.WriteLine(indexesLineString);
        }

        private static void printRowOfCards<T>(ref PlayingCards<T>[,] i_GameBoard, int i_RowIndex)
        {
            T PlayCard;
            StringBuilder rowOfCards = new StringBuilder();
            int k = 1; // TODO: Make it smart k Var
            for (int colIndex = 0; colIndex < i_GameBoard.GetLength(0); colIndex++)
            {
                if (i_GameBoard[i_RowIndex, colIndex].IsVisible)
                {
                    PlayCard = i_GameBoard[i_RowIndex, colIndex].CardValue;
                    rowOfCards.Append($" {PlayCard.ToString()} {k_ColumnSeparator}");
                }
                else
                {
                    rowOfCards.Append(' ', k); // TODO: Make it smart k Var
                }
            }

            Console.WriteLine(rowOfCards);
        }

        private static void printRowForSeparation(int i_Width)
        {
            const int k = 4; // TODO: Change it to smart var
            StringBuilder rowOfSepration = new StringBuilder();

            for (int colIndex = 0; colIndex < i_Width; colIndex++)
            {
                rowOfSepration.Append('=', k); // TODO: Change it to smart var
            }

            Console.WriteLine(rowOfSepration);
        }

        internal static string askForUserInput(string i_MessageForUser)
        {
            Console.WriteLine(i_MessageForUser);
            return Console.ReadLine();
        }

        internal static bool validateYesOrNoInput(string i_UserInput)
        {
            string userInputInUpperCase = i_UserInput.ToUpper();
            return userInputInUpperCase == "Y" || userInputInUpperCase == "N";
        }

        internal static bool askUserForYesOrNoQuestion(string i_questionForUser)
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

        internal static int askUserForIntBetweenValues(string i_questionForUser, int i_MinValue, int i_MaxValue)
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
