namespace board.chess
{
    internal class King : Piece
    {
        public King(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "K";
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] matrixOfPossibilities = new bool[Board.Rows, Board.Columns];
            
            Position positionToCheck = new(0, 0);

            // North direction
            positionToCheck.DefineValues(Position.Row - 1, Position.Column);
            if (PossiblePosition(positionToCheck))
            {
                matrixOfPossibilities[positionToCheck.Row, positionToCheck.Column] = true;
            }

            // Northeast direction
            positionToCheck.DefineValues(Position.Row - 1, Position.Column + 1);
            if (PossiblePosition(positionToCheck))
            {
                matrixOfPossibilities[positionToCheck.Row, positionToCheck.Column] = true;
            }

            // East direction
            positionToCheck.DefineValues(Position.Row, Position.Column + 1);
            if (PossiblePosition(positionToCheck))
            {
                matrixOfPossibilities[positionToCheck.Row, positionToCheck.Column] = true;
            }

            // Southeast direction
            positionToCheck.DefineValues(Position.Row + 1, Position.Column + 1);
            if (PossiblePosition(positionToCheck))
            {
                matrixOfPossibilities[positionToCheck.Row, positionToCheck.Column] = true;
            }

            // Southern direction
            positionToCheck.DefineValues(Position.Row + 1, Position.Column);
            if (PossiblePosition(positionToCheck))
            {
                matrixOfPossibilities[positionToCheck.Row, positionToCheck.Column] = true;
            }

            // Southwest direction
            positionToCheck.DefineValues(Position.Row + 1, Position.Column - 1);
            if (PossiblePosition(positionToCheck))
            {
                matrixOfPossibilities[positionToCheck.Row, positionToCheck.Column] = true;
            }

            // West direction
            positionToCheck.DefineValues(Position.Row, Position.Column - 1);
            if (PossiblePosition(positionToCheck))
            {
                matrixOfPossibilities[positionToCheck.Row, positionToCheck.Column] = true;
            }

            // Northwest direction
            positionToCheck.DefineValues(Position.Row - 1, Position.Column - 1);
            if (PossiblePosition(positionToCheck))
            {
                matrixOfPossibilities[positionToCheck.Row, positionToCheck.Column] = true;
            }

            return matrixOfPossibilities;
        }

        private bool CanMove(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece == null || piece.Color != Color;
        }

        private bool PossiblePosition(Position position) => Board.IsPositionValid(position) && CanMove(position);
    }
}
