using board;

namespace board.chess
{
    internal class ChessMatch
    {
        public Board Board { get; private set; }
        private int _turn;
        private Color _currentPlayer;
        public bool IsFinished { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            _turn = 1;
            _currentPlayer = Color.White;
            IsFinished = false;
            PlacePieces();
        }

        public void ExecuteMovement(Position origin, Position destination)
        {
            Piece? piece = Board.RemoveAPiece(origin);
            if (piece != null)
            {
                piece.IncreaseNumberOfMovements();
                Piece? capturedPiece = Board.RemoveAPiece(destination);
                Board.PlaceAPiece(piece, destination);
            }
        }

        private void PlacePieces()
        {
            Board.PlaceAPiece(new Rook(Board, Color.White), new ChessPosition('c', 1).ToPosition());
            Board.PlaceAPiece(new Rook(Board, Color.White), new ChessPosition('c', 2).ToPosition());
            Board.PlaceAPiece(new Rook(Board, Color.White), new ChessPosition('d', 2).ToPosition());
            Board.PlaceAPiece(new Rook(Board, Color.White), new ChessPosition('e', 2).ToPosition());
            Board.PlaceAPiece(new Rook(Board, Color.White), new ChessPosition('e', 1).ToPosition());
            Board.PlaceAPiece(new King(Board, Color.White), new ChessPosition('d', 1).ToPosition());

            Board.PlaceAPiece(new Rook(Board, Color.Black), new ChessPosition('c', 7).ToPosition());
            Board.PlaceAPiece(new Rook(Board, Color.Black), new ChessPosition('c', 8).ToPosition());
            Board.PlaceAPiece(new Rook(Board, Color.Black), new ChessPosition('d', 7).ToPosition());
            Board.PlaceAPiece(new Rook(Board, Color.Black), new ChessPosition('e', 7).ToPosition());
            Board.PlaceAPiece(new Rook(Board, Color.Black), new ChessPosition('e', 8).ToPosition());
            Board.PlaceAPiece(new King(Board, Color.Black), new ChessPosition('d', 8).ToPosition());
        }
    }
}
