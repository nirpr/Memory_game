using System;

namespace MemoryGameInterface
{
    public class GameInterface
    {

        public void StartGame()
        {
            bool gameIsStillPlaying = true;

            while (gameIsStillPlaying)
            {
                // Show Board
                // Show Current Player Msg

                // Player Move #1

                // Show Board, Player move update #1

                // Player Move #2
                
                // Show Board, Player move update #2 - 2 seconds for wrong

                // if GameStatus = { END or Process }
                // - END: Show GameOver -> Announce Winner -> Ask for restart game
                // - Process: skip for next round.
            }
        }

        public void makeGuess()
        {
            // if current player is computer
            // - make random guess by GameLogic

            // else (player is not computer)
            
            // while(move is not valid)
            
            // - get guess from user
            
            // - player choose quit, finish the game
            
            // - player made a illegal move announce it and try again.
        }

        public void askUserForBoardSize()
        {

        }

        public bool askUserForRestartGame()
        {
            const bool k_WaitingTillValidInput = true;

            while (k_WaitingTillValidInput)
            {
                string userInput = askForUserInput("Do you want to play again? [Y/N]");
                bool isInputValid = validateYesOrNoInput(userInput);

                if (isInputValid)
                {
                    return true; // TODO: Make this part send answer from user input
                }
                else
                {
                    Console.WriteLine("Invalid response, Try again !");
                }
            }
        }

        private string askForUserInput(string i_MessageForUser)
        {
            Console.WriteLine(i_MessageForUser);
            return Console.ReadLine();
        }

        private static bool validateYesOrNoInput(string i_UserInput)
        {
            string userInputInUpperCase = i_UserInput.ToUpper();
            return userInputInUpperCase == "Y" || userInputInUpperCase == "N";
        }

        private void setPlayers()
        {
            setFirstPlayer();
            setSecondPlayer();
        }

        private void setFirstPlayer()
        {

        }

        private void setSecondPlayer()
        {
            const bool k_WaitingTillValidInput = true;

            while (k_WaitingTillValidInput)
            {
                string userInput = askForUserInput("Do you want play against the computer? [Y/N]: ");
                bool isInputValid = validateYesOrNoInput(userInput);
                if (isInputValid)
                {
                    bool userAnswer = false; // TODO: need make it yes / no converting to bool
                    // set player 2
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again!");
                }
            }
        }
    }

}
