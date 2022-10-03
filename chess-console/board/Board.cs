using exceptions;

namespace board
{
    internal class Board
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        private Piece[,] _pieces;

        public Board(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            _pieces = new Piece[rows, columns];
        }

        public Piece Piece(int row, int column)
        {
            return _pieces[row, column];
        }

        public Piece Piece(Position position)
        {
            return _pieces[position.Row, position.Column];
        }

        public bool IsThereAPiece(Position position)
        {
            CheckPosition(position);
            return Piece(position) != null;
        }
        
        public void PlaceAPiece(Piece piece, Position position)
        {
            if (IsThereAPiece(position))
            {
                throw new BoardException($"There is already a piece in the position ({position})!");
            }
            _pieces[position.Row, position.Column] = piece;
            piece.Position = position;
        }

        public Piece? RemoveAPiece(Position position)
        {
            if (Piece(position) == null)
            {
                return null;
            }
            Piece removedPiece = Piece(position);
            removedPiece.Position = null;
            _pieces[position.Row, position.Column] = null;
            return removedPiece;
        }

        public bool IsPositionValid(Position position)
        {
            if (position.Row < 0 || position.Row >= Rows || position.Column < 0 || position.Column >= Columns)
            {
                return false;
            }
            
            return true;
        }

        public void CheckPosition(Position position)
        {
            if (!IsPositionValid(position))
            {
                throw new BoardException($"Position ({position}) is invalid!");
            }
        }
    }
}
