using board;

namespace chess_console
{
    internal class Screen
    {
        public static void PrintBoard(Board board)
        {
            for (int i = 0; i < board.Rows; i++)
            {
                PrintRowNumbers(board.Rows, i);
                for (int j = 0; j < board.Columns; j++)
                {
                    if (board.Piece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        PrintPiece(board.Piece(i, j));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.Write("  ");
            PrintColumnLetters(board.Columns);
        }

        private static void PrintRowNumbers(int numberOfRows, int rowIndex)
        {
            Console.Write(numberOfRows - rowIndex + " ");
        }

        private static void PrintColumnLetters(int numberOfColumns)
        {
            for (int i = 0; i < numberOfColumns; i++)
            {
                Console.Write((char)(i + 'A') + " ");
            }
        }

        private static void PrintPiece(Piece piece)
        {
            if (piece.Color == Color.White)
            {
                Console.Write(piece);
            }
            else
            {
                ConsoleColor auxiliaryConsoleColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(piece);
                Console.ForegroundColor = auxiliaryConsoleColor;
            }
        }
    }
}
