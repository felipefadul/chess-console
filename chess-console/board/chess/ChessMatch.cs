using exceptions;

namespace board.chess
{
    internal class ChessMatch
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool IsFinished { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            IsFinished = false;
            PlacePieces();
        }

        public void MakeAMove(Position origin, Position destination)
        {
            ExecuteMovement(origin, destination);
            Turn++;
            SwitchPlayer();
        }

        public void ValidateOriginPosition(Position origin)
        {
            if (Board.Piece(origin) == null)
            {
                throw new BoardException("There is no piece in the chosen origin position!");
            }
            
            if (CurrentPlayer != Board.Piece(origin).Color)
            {
                throw new BoardException("The origin piece chosen is not yours!");
            }

            if (!Board.Piece(origin).HasPossibleMovements())
            {
                throw new BoardException("There are no possible movements for the chosen origin piece!");
            }
        }

        public void ValidateDestinationPosition(Position origin, Position destination)
        {
            if (!Board.Piece(origin).CanMoveToPosition(destination))
            {
                throw new BoardException("Invalid destination position!");
            }
        }

        private void ExecuteMovement(Position origin, Position destination)
        {
            Piece? piece = Board.RemoveAPiece(origin);
            if (piece != null)
            {
                piece.IncreaseNumberOfMovements();
                Piece? capturedPiece = Board.RemoveAPiece(destination);
                Board.PlaceAPiece(piece, destination);
            }
        }

        private void SwitchPlayer()
        {
            CurrentPlayer = CurrentPlayer == Color.White ? Color.Black : Color.White;
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
