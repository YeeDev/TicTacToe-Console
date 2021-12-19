using System;

namespace TicTacToe
{
    class Program
    {
        static int turn;
        static bool aPlayerWon;
        static string input;
        static string player = "O";
        static string messageToPrint = "";

        static string[,] matrix = { { "1", "2", "3" }, { "4", "5", "6" }, { "7", "8", "9" } };

        static void Main(string[] args)
        {
            StartGame();
        }

        private static void StartGame()
        {
            messageToPrint = $"Player {player}: Choose your field! : ";
            PrintBoard();
            RunMainGame();
        }

        private static void PrintBoard()
        {
            Console.Clear();

            Console.WriteLine(
                "     |     |     \n" +
                "  {0}  |  {1}  |  {2}  \n" +
                "     |     |     \n" +
                "----- ----- -----\n" +
                 "     |     |     \n" +
                "  {3}  |  {4}  |  {5}  \n" +
                "     |     |     \n" +
                "----- ----- -----\n" +
                 "     |     |     \n" +
                "  {6}  |  {7}  |  {8}  \n" +
                "     |     |     \n",
                matrix[0, 0], matrix[0, 1], matrix[0, 2],
                matrix[1, 0], matrix[1, 1], matrix[1, 2],
                matrix[2, 0], matrix[2, 1], matrix[2, 2]
                );

            WriteInstructions();
        }

        private static void WriteInstructions()
        {
            Console.WriteLine();
            Console.Write(messageToPrint);
        }

        private static void RunMainGame()
        {
            while (!HasSomeoneWon() && turn < 9)
            {
                ForceValidInput();
                SetMatrixValueToPlayerInput();
                ChangeTurn();
            }

            EndGameMessage();

            Console.Read();

            ResetGame();
        }

        private static void ForceValidInput()
        {
            while (!IsInputValid())
            {
                messageToPrint = "Invalid entry, please enter a number between 1 and 9 that hasn't been selected yet: ";
                PrintBoard();
            }
        }

        private static bool IsInputValid()
        {
            input = Console.ReadLine();

            int fieldSelected;
            if (!int.TryParse(input, out fieldSelected) || fieldSelected < 1 || fieldSelected > 9) { return false; }

            foreach (string s in matrix)
            {
                fieldSelected--;
                if (fieldSelected == 0 && (s == "O" || s == "X")) { return false; }
            }

            return true;
        }

        private static void SetMatrixValueToPlayerInput()
        {
            for (int x = 0; x < matrix.GetLength(0); x++)
            {
                for (int y = 0; y < matrix.GetLength(1); y++)
                {
                    if (matrix[y, x] == input)
                    {
                        matrix[y, x] = player;
                        break;
                    }
                }
            }
        }

        private static void ChangeTurn()
        {
            turn++;
            player = player == "X" ? "O" : "X";
            messageToPrint = $"Player {player}: Choose your field! : ";
            PrintBoard();
        }

        private static bool HasSomeoneWon()
        {
            int columnCounter = 0;
            int rowCounter = 0;
            int rightToLeft = 0;
            int leftToRight = 0;

            for (int x = 0, j = 2; x < matrix.GetLength(0); x++, j--)
            {
                rightToLeft += matrix[x, x] == "O" ? 1 : matrix[x, x] == "X" ? -1 : 0;
                leftToRight += matrix[x, j] == "O" ? 1 : matrix[x, j] == "X" ? -1 : 0;

                for (int y = 0; y < matrix.GetLength(1); y++)
                {
                    columnCounter += matrix[y, x] == "O" ? 1 : matrix[y, x] == "X" ? -1 : 0;
                    rowCounter += matrix[x, y] == "O" ? 1 : matrix[x, y] == "X" ? -1 : 0;
                }

                if (Math.Abs(rowCounter) == 3 || Math.Abs(columnCounter) == 3 ||
                    Math.Abs(rightToLeft) == 3 || Math.Abs(leftToRight) == 3)
                {
                    aPlayerWon = true;
                    return true;
                } 

                columnCounter = 0;
                rowCounter = 0;
            }

            return false;
        }

        private static void EndGameMessage()
        {
            if (aPlayerWon)
            {
                player = player == "X" ? "O" : "X";
                messageToPrint = $"Game Has Ended! Player {player} won the game!";
            }
            else
            {
                messageToPrint = $"Game Has Ended In A Tie!";
            }

            PrintBoard();
            Console.Write("\nPress Enter to reset the game.");
        }

        private static void ResetGame()
        {
            int counter = 1;
            for (int x = 0; x < matrix.GetLength(0); x++)
            {
                for (int y = 0; y < matrix.GetLength(1); y++)
                {
                    matrix[x, y] = counter.ToString();
                    counter++;
                }
            }

            turn = 0;
            aPlayerWon = false;
            player = "O";

            StartGame();
        }
    }
}
