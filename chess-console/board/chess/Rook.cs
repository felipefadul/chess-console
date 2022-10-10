namespace board.chess
{
    internal class Rook : Piece
    {
        public Rook(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "R";
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] matrixOfPossibilities = new bool[Board.Rows, Board.Columns];

            Position positionToCheck = new(0, 0);

            // Forward direction
            positionToCheck.DefineValues(Position.Row - 1, Position.Column);
            while (PossiblePosition(positionToCheck))
            {
                matrixOfPossibilities[positionToCheck.Row, positionToCheck.Column] = true;
                if (Board.Piece(positionToCheck) != null && Board.Piece(positionToCheck).Color != Color)
                {
                    break;
                }
                positionToCheck.Row--;
            }

            // Backward direction
            positionToCheck.DefineValues(Position.Row + 1, Position.Column);
            while (PossiblePosition(positionToCheck))
            {
                matrixOfPossibilities[positionToCheck.Row, positionToCheck.Column] = true;
                if (Board.Piece(positionToCheck) != null && Board.Piece(positionToCheck).Color != Color)
                {
                    break;
                }
                positionToCheck.Row++;
            }

            // Right direction
            positionToCheck.DefineValues(Position.Row, Position.Column + 1);
            while (PossiblePosition(positionToCheck))
            {
                matrixOfPossibilities[positionToCheck.Row, positionToCheck.Column] = true;
                if (Board.Piece(positionToCheck) != null && Board.Piece(positionToCheck).Color != Color)
                {
                    break;
                }
                positionToCheck.Column++;
            }

            // Left direction
            positionToCheck.DefineValues(Position.Row, Position.Column - 1);
            while (PossiblePosition(positionToCheck))
            {
                matrixOfPossibilities[positionToCheck.Row, positionToCheck.Column] = true;
                if (Board.Piece(positionToCheck) != null && Board.Piece(positionToCheck).Color != Color)
                {
                    break;
                }
                positionToCheck.Column--;
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
