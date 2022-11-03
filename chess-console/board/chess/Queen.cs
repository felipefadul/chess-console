namespace board.chess
{
    internal class Queen : Piece
    {
        public Queen(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "Q";
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

            // Northwest direction
            positionToCheck.DefineValues(Position.Row - 1, Position.Column - 1);
            while (PossiblePosition(positionToCheck))
            {
                matrixOfPossibilities[positionToCheck.Row, positionToCheck.Column] = true;
                if (Board.Piece(positionToCheck) != null && Board.Piece(positionToCheck).Color != Color)
                {
                    break;
                }
                positionToCheck.DefineValues(positionToCheck.Row - 1, positionToCheck.Column - 1);
            }

            // Northeast direction
            positionToCheck.DefineValues(Position.Row - 1, Position.Column + 1);
            while (PossiblePosition(positionToCheck))
            {
                matrixOfPossibilities[positionToCheck.Row, positionToCheck.Column] = true;
                if (Board.Piece(positionToCheck) != null && Board.Piece(positionToCheck).Color != Color)
                {
                    break;
                }
                positionToCheck.DefineValues(positionToCheck.Row - 1, positionToCheck.Column + 1);
            }

            // Southeast direction
            positionToCheck.DefineValues(Position.Row + 1, Position.Column + 1);
            while (PossiblePosition(positionToCheck))
            {
                matrixOfPossibilities[positionToCheck.Row, positionToCheck.Column] = true;
                if (Board.Piece(positionToCheck) != null && Board.Piece(positionToCheck).Color != Color)
                {
                    break;
                }
                positionToCheck.DefineValues(positionToCheck.Row + 1, positionToCheck.Column + 1);
            }

            // Southwest direction
            positionToCheck.DefineValues(Position.Row + 1, Position.Column - 1);
            while (PossiblePosition(positionToCheck))
            {
                matrixOfPossibilities[positionToCheck.Row, positionToCheck.Column] = true;
                if (Board.Piece(positionToCheck) != null && Board.Piece(positionToCheck).Color != Color)
                {
                    break;
                }
                positionToCheck.DefineValues(positionToCheck.Row + 1, positionToCheck.Column - 1);
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
