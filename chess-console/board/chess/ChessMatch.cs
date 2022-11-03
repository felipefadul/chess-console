using exceptions;

namespace board.chess
{
    internal class ChessMatch
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool IsFinished { get; private set; }
        public bool IsInCheck { get; private set; }
        private HashSet<Piece> _piecesOnTheBoard;
        private HashSet<Piece> _capturedPieces;
        
        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            IsFinished = false;
            IsInCheck = false;
            _piecesOnTheBoard = new HashSet<Piece>();
            _capturedPieces = new HashSet<Piece>();
            PlacePieces();
        }

        public void MakeAMove(Position origin, Position destination)
        {
            Piece? capturedPiece = ExecuteMovement(origin, destination);

            if (ValidateIfKingIsInCheck(CurrentPlayer))
            {
                UndoMovement(origin, destination, capturedPiece);
                throw new BoardException("You can't put yourself in check!");
            }

            Color opponentPlayer = GetOpposingPlayer(CurrentPlayer);

            IsInCheck = ValidateIfKingIsInCheck(opponentPlayer);

            if (ValidateIfIsCheckmate(opponentPlayer))
            {
                IsFinished = true;
            }
            else
            {
                Turn++;
                SwitchPlayer();
            }
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
            if (!Board.Piece(origin).ValidatePossibleMovementToPosition(destination))
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

        private bool ValidateIfKingIsInCheck(Color color)
        {
            Piece? king = GetKing(color);

            if (king == null || king.Position == null)
            {
                throw new BoardException($"There is no king of the color {color} on the board!");
            }

            Color opponentColor = GetOpposingPlayer(color);

            foreach (Piece opponentPiece in GetPiecesOnTheBoardByColor(opponentColor))
            {
                bool[,] matrixOfPossibilities = opponentPiece.PossibleMovements();
                if (matrixOfPossibilities[king.Position.Row, king.Position.Column])
                {
                    return true;
                }
            }

            return false;
        }

        private bool ValidateIfIsCheckmate(Color color)
        {
            if (!ValidateIfKingIsInCheck(color))
            {
                return false;
            }
            
            foreach (Piece piece in GetPiecesOnTheBoardByColor(color))
            {
                bool[,] matrixOfPossibilities = piece.PossibleMovements();
                for (int i = 0; i < Board.Rows; i++)
                {
                    for (int j = 0; j < Board.Columns; j++)
                    {
                        if (matrixOfPossibilities[i, j])
                        {
                            Position origin = piece.Position;
                            Position destination = new(i, j);
                            Piece? capturedPiece = ExecuteMovement(origin, destination);
                            bool isInCheck = ValidateIfKingIsInCheck(color);
                            UndoMovement(origin, destination, capturedPiece);
                            if (!isInCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        private Piece? ExecuteMovement(Position origin, Position destination)
        {
            Piece? piece = Board.RemoveAPiece(origin);
            if (piece == null)
            {
                return null;
            }

            piece.IncreaseNumberOfMovements();
            Piece? capturedPiece = Board.RemoveAPiece(destination);
            Board.PlaceAPiece(piece, destination);
            if (capturedPiece != null)
            {
                _capturedPieces.Add(capturedPiece);
            }

            return capturedPiece;
        }

        private void UndoMovement(Position origin, Position destination, Piece? capturedPiece)
        {
            Piece? piece = Board.RemoveAPiece(destination);
            if (piece == null)
            {
                return;
            }

            piece.DecreaseNumberOfMovements();

            if (capturedPiece != null)
            {
                Board.PlaceAPiece(capturedPiece, destination);
                _capturedPieces.Remove(capturedPiece);
            }

            Board.PlaceAPiece(piece, origin);
        }

        private void SwitchPlayer()
        {
            CurrentPlayer = CurrentPlayer == Color.White ? Color.Black : Color.White;
        }

        private Color GetOpposingPlayer(Color color)
        {
            return color == Color.White ? Color.Black : Color.White;
        }

        private Piece? GetKing(Color color)
        {
            return GetPiecesOnTheBoardByColor(color).FirstOrDefault(piece => piece is King);
        }

        private void PlacePieces()
        {
            PlaceNewPiece('a', 1, new Rook(Board, Color.White));
            PlaceNewPiece('b', 1, new Knight(Board, Color.White));
            PlaceNewPiece('c', 1, new Bishop(Board, Color.White));
            PlaceNewPiece('d', 1, new Queen(Board, Color.White));
            PlaceNewPiece('e', 1, new King(Board, Color.White));
            PlaceNewPiece('f', 1, new Bishop(Board, Color.White));
            PlaceNewPiece('g', 1, new Knight(Board, Color.White));
            PlaceNewPiece('h', 1, new Rook(Board, Color.White));
            PlaceNewPiece('a', 2, new Pawn(Board, Color.White));
            PlaceNewPiece('b', 2, new Pawn(Board, Color.White));
            PlaceNewPiece('c', 2, new Pawn(Board, Color.White));
            PlaceNewPiece('d', 2, new Pawn(Board, Color.White));
            PlaceNewPiece('e', 2, new Pawn(Board, Color.White));
            PlaceNewPiece('f', 2, new Pawn(Board, Color.White));
            PlaceNewPiece('g', 2, new Pawn(Board, Color.White));
            PlaceNewPiece('h', 2, new Pawn(Board, Color.White));

            PlaceNewPiece('a', 8, new Rook(Board, Color.Black));
            PlaceNewPiece('b', 8, new Knight(Board, Color.Black));
            PlaceNewPiece('c', 8, new Bishop(Board, Color.Black));
            PlaceNewPiece('d', 8, new Queen(Board, Color.Black));
            PlaceNewPiece('e', 8, new King(Board, Color.Black));
            PlaceNewPiece('f', 8, new Bishop(Board, Color.Black));
            PlaceNewPiece('g', 8, new Knight(Board, Color.Black));
            PlaceNewPiece('h', 8, new Rook(Board, Color.Black));
            PlaceNewPiece('a', 7, new Pawn(Board, Color.Black));
            PlaceNewPiece('b', 7, new Pawn(Board, Color.Black));
            PlaceNewPiece('c', 7, new Pawn(Board, Color.Black));
            PlaceNewPiece('d', 7, new Pawn(Board, Color.Black));
            PlaceNewPiece('e', 7, new Pawn(Board, Color.Black));
            PlaceNewPiece('f', 7, new Pawn(Board, Color.Black));
            PlaceNewPiece('g', 7, new Pawn(Board, Color.Black));
            PlaceNewPiece('h', 7, new Pawn(Board, Color.Black));
        }
    }
}
