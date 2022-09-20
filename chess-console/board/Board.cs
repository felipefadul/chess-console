using exceptions;

namespace board
{
    internal class Board
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        private Piece[,] Pieces;

        public Board(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Pieces = new Piece[rows, columns];
        }

        public Piece Piece(int row, int column)
        {
            return Pieces[row, column];
        }

        public Piece Piece(Position position)
        {
            return Pieces[position.Row, position.Column];
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
            Pieces[position.Row, position.Column] = piece;
            piece.Position = position;
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
