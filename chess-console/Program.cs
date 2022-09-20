using board;
using board.chess;
using exceptions;

namespace chess_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ChessPosition chessPosition = new('c', 7);
            Console.WriteLine(chessPosition);
            Console.WriteLine(chessPosition.ToPosition());

            Console.ReadLine();
        }
    }
}