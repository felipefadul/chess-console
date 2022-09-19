using board;
using board.chess;

namespace chess_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);

            board.PlaceAPiece(new Rook(board, Color.Black), new Position(0, 0));
            board.PlaceAPiece(new Rook(board, Color.Black), new Position(1, 3));
            board.PlaceAPiece(new King(board, Color.Black), new Position(2, 4));

            Screen.PrintBoard(board);
            Console.ReadLine();
        }
    }
}