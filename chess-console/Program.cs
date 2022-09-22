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
                Board board = new (8, 8);
                board.PlaceAPiece(new Rook(board, Color.Black), new Position(0, 0));
                board.PlaceAPiece(new Rook(board, Color.Black), new Position(1, 3));
                board.PlaceAPiece(new King(board, Color.Black), new Position(0, 2));
                board.PlaceAPiece(new Rook(board, Color.White), new Position(3, 5));

                Screen.PrintBoard(board);
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}