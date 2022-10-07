using board;
using board.chess;
using exceptions;

namespace chess_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessMatch match = new();
                while (!match.IsFinished)
                {
                    Console.Clear();
                    Screen.PrintBoard(match.Board);

                    Console.WriteLine(Environment.NewLine);
                    Console.Write("Origin: ");
                    Position origin = Screen.ReadChessPosition().ToPosition();

                    bool[,] possibleMovements = match.Board.Piece(origin).PossibleMovements();

                    Console.Clear();
                    Screen.PrintBoard(match.Board, possibleMovements);

                    Console.WriteLine(Environment.NewLine);
                    Console.Write("Destination: ");
                    Position destination = Screen.ReadChessPosition().ToPosition();

                    match.ExecuteMovement(origin, destination);
                }
                
                Screen.PrintBoard(match.Board);
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}