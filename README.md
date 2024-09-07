# Memory Game

## Description
This is a classic memory card game that can be played either as a 1v1 (player vs. player) or player vs. computer. The game is implemented in C# using the .NET Framework and follows Object-Oriented Programming (OOP) principles.

The project is divided into two distinct parts:
1. **Game Interface**: Handles the user interface and presents the game to the player.
2. **Game Logic**: Manages the core functionality of the game, including game rules, player turns, and victory conditions.

The two parts are fully separated. The game interface only interacts with the game logic by requesting the necessary data to display to the user.

## Features
- **1v1 Mode**: Play against another human player.
- **Player vs. Computer Mode**: Challenge yourself against an AI opponent.
- **OOP Design**: Clean and maintainable code with a clear separation of concerns.
  
## Installation
1. Clone this repository to your local machine:
   ```bash
   git clone https://github.com/nirpr/Memory_game.git
2. Open the solution in Visual Studio.
3. Build the solution to restore any missing NuGet packages.
4. Run the game.

## Usage
- **Start a New Game**: Choose between 1v1 mode or player vs. computer mode.   
- **Gameplay**: Players take turns flipping two cards, trying to find matching pairs. The player who finds the most pairs wins the game.

## Project Structure
- **GameInterface**: This project handles all the UI elements. It does not contain any game logic but communicates with the GameLogic project to retrieve data.
- **GameLogic**: This project manages the core logic of the game, such as shuffling the cards, managing player turns, and determining the winner.

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.











