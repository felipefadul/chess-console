using board;
using board.chess;

namespace chess_console
{
    internal class Screen
    {
        public static void PrintMatch(ChessMatch match)
        {
            PrintBoard(match.Board);
            Console.WriteLine(Environment.NewLine);
            PrintCapturedPieces(match);
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Turn: " + match.Turn);
            if (!match.IsFinished)
            {
                Console.WriteLine("Waiting player: " + match.CurrentPlayer);
                if (match.IsInCheck)
                {
                    Console.WriteLine("CHECK!");
                }
            }
            else
            {
                Console.WriteLine("CHECKMATE!");
                Console.WriteLine("Winner: " + match.CurrentPlayer);
            }
        }

        public static void PrintBoard(Board board)
        {
            for (int i = 0; i < board.Rows; i++)
            {
                PrintRowNumbers(board.Rows, i);
                for (int j = 0; j < board.Columns; j++)
                {
                    PrintPieceOrEmptySpace(board.Piece(i, j));
                }
                Console.WriteLine();
            }
            Console.Write("  ");
            PrintColumnLetters(board.Columns);
        }

        public static void PrintBoard(Board board, bool[,] possibleMovements)
        {
            ConsoleColor originalBackground = Console.BackgroundColor;

            for (int i = 0; i < board.Rows; i++)
            {
                PrintRowNumbers(board.Rows, i);
                for (int j = 0; j < board.Columns; j++)
                {
                    PrintBackgroundColor(possibleMovements[i, j], originalBackground);
                    PrintPieceOrEmptySpace(board.Piece(i, j));
                    Console.BackgroundColor = originalBackground;
                }
                Console.WriteLine();
            }
            Console.Write("  ");
            PrintColumnLetters(board.Columns);
            Console.BackgroundColor = originalBackground;
        }

        public static ChessPosition ReadChessPosition()
        {
            string input = Console.ReadLine();
            char column = input[0];
            int row = int.Parse(input[1] + "");

            return new ChessPosition(column, row);
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

        private static void PrintPieceOrEmptySpace(Piece piece)
        {
            if (piece == null)
            {
                PrintEmptyPieceSpace();
            }
            else
            {
                PrintPiece(piece);
                Console.Write(" ");
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

        private static void PrintEmptyPieceSpace()
        {
            Console.Write("- ");
        }

        private static void PrintBackgroundColor(bool isPossibleMovement, ConsoleColor originalBackground)
        {
            Console.BackgroundColor = isPossibleMovement ? ConsoleColor.DarkGray : originalBackground;
        }

        private static void PrintCapturedPieces(ChessMatch match)
        {
            Console.WriteLine("Captured pieces:");
            Console.Write("White: ");
            PrintSet(match.GetCapturedPiecesByColor(Color.White));
            Console.WriteLine();
            Console.Write("Black: ");
            ConsoleColor auxiliaryConsoleColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintSet(match.GetCapturedPiecesByColor(Color.Black));
            Console.ForegroundColor = auxiliaryConsoleColor;
        }

        private static void PrintSet(HashSet<Piece> set)
        {
            Console.Write("[");
            foreach (Piece piece in set)
            {
                Console.Write(piece + " ");
            }
            Console.Write("]");
        }
    }
}
