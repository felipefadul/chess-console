namespace board.chess
{
    internal class Knight : Piece
    {
        public Knight(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "N";
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] matrixOfPossibilities = new bool[Board.Rows, Board.Columns];
            
            Position positionToCheck = new(0, 0);

            positionToCheck.DefineValues(Position.Row - 1, Position.Column - 2);
            if (PossiblePosition(positionToCheck))
            {
                matrixOfPossibilities[positionToCheck.Row, positionToCheck.Column] = true;
            }

            positionToCheck.DefineValues(Position.Row - 2, Position.Column - 1);
            if (PossiblePosition(positionToCheck))
            {
                matrixOfPossibilities[positionToCheck.Row, positionToCheck.Column] = true;
            }

            positionToCheck.DefineValues(Position.Row - 2, Position.Column + 1);
            if (PossiblePosition(positionToCheck))
            {
                matrixOfPossibilities[positionToCheck.Row, positionToCheck.Column] = true;
            }

            positionToCheck.DefineValues(Position.Row - 1, Position.Column + 2);
            if (PossiblePosition(positionToCheck))
            {
                matrixOfPossibilities[positionToCheck.Row, positionToCheck.Column] = true;
            }

            positionToCheck.DefineValues(Position.Row + 1, Position.Column + 2);
            if (PossiblePosition(positionToCheck))
            {
                matrixOfPossibilities[positionToCheck.Row, positionToCheck.Column] = true;
            }

            positionToCheck.DefineValues(Position.Row + 2, Position.Column + 1);
            if (PossiblePosition(positionToCheck))
            {
                matrixOfPossibilities[positionToCheck.Row, positionToCheck.Column] = true;
            }

            positionToCheck.DefineValues(Position.Row + 2, Position.Column - 1);
            if (PossiblePosition(positionToCheck))
            {
                matrixOfPossibilities[positionToCheck.Row, positionToCheck.Column] = true;
            }

            positionToCheck.DefineValues(Position.Row + 1, Position.Column - 2);
            if (PossiblePosition(positionToCheck))
            {
                matrixOfPossibilities[positionToCheck.Row, positionToCheck.Column] = true;
            }

            return matrixOfPossibilities;
        }

        private bool CanMoveToPosition(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece == null || piece.Color != Color;
        }

        private bool PossiblePosition(Position position) => Board.IsPositionValid(position) && CanMoveToPosition(position);
    }
}
