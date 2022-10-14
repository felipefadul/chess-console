using exceptions;

namespace board.chess
{
    internal class ChessMatch
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool IsFinished { get; private set; }
        private HashSet<Piece> _piecesOnTheBoard;
        private HashSet<Piece> _capturedPieces;

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            IsFinished = false;
            _piecesOnTheBoard = new HashSet<Piece>();
            _capturedPieces = new HashSet<Piece>();
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

        public void PlaceNewPiece(char column, int row, Piece piece)
        {
            Board.PlaceAPiece(piece, new ChessPosition(column, row).ToPosition());
            _piecesOnTheBoard.Add(piece);
        }

        public HashSet<Piece> GetCapturedPiecesByColor(Color color)
        {
            return _capturedPieces.Where(capturedPiece => capturedPiece.Color == color).ToHashSet();
        }

        public HashSet<Piece> GetPiecesOnTheBoardByColor(Color color)
        {
            return _piecesOnTheBoard.Where(piece => piece.Color == color && !_capturedPieces.Contains(piece)).ToHashSet();
        }

        private void ExecuteMovement(Position origin, Position destination)
        {
            Piece? piece = Board.RemoveAPiece(origin);
            if (piece == null)
            {
                return;
            }
            
            piece.IncreaseNumberOfMovements();
            Piece? capturedPiece = Board.RemoveAPiece(destination);
            Board.PlaceAPiece(piece, destination);
            if (capturedPiece != null)
            {
                _capturedPieces.Add(capturedPiece);
            }
        }

        private void SwitchPlayer()
        {
            CurrentPlayer = CurrentPlayer == Color.White ? Color.Black : Color.White;
        }

        private void PlacePieces()
        {
            PlaceNewPiece('c', 1, new Rook(Board, Color.White));
            PlaceNewPiece('c', 2, new Rook(Board, Color.White));
            PlaceNewPiece('d', 1, new King(Board, Color.White));
            PlaceNewPiece('d', 2, new Rook(Board, Color.White));
            PlaceNewPiece('e', 1, new Rook(Board, Color.White));
            PlaceNewPiece('e', 2, new Rook(Board, Color.White));

            PlaceNewPiece('c', 7, new Rook(Board, Color.Black));
            PlaceNewPiece('c', 8, new Rook(Board, Color.Black));
            PlaceNewPiece('d', 7, new Rook(Board, Color.Black));
            PlaceNewPiece('d', 8, new King(Board, Color.Black));
            PlaceNewPiece('e', 7, new Rook(Board, Color.Black));
            PlaceNewPiece('e', 8, new Rook(Board, Color.Black));
        }
    }
}
