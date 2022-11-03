namespace board.chess
{
    internal class Pawn : Piece
    {
        public Pawn(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "P";
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] matrixOfPossibilities = new bool[Board.Rows, Board.Columns];
            
            Position positionToCheck = new(0, 0);

            if (Color == Color.White)
            {
                positionToCheck.DefineValues(Position.Row - 1, Position.Column);
                if (Board.IsPositionValid(positionToCheck) && IsPositionFree(positionToCheck))
                {
                    matrixOfPossibilities[positionToCheck.Row, positionToCheck.Column] = true;
                }
                
                positionToCheck.DefineValues(Position.Row - 2, Position.Column);
                Position secondPositionToCheck = new Position(Position.Row - 1, Position.Column);
                if (Board.IsPositionValid(secondPositionToCheck) && IsPositionFree(secondPositionToCheck) && Board.IsPositionValid(positionToCheck) && IsPositionFree(positionToCheck) && NumberOfMovements == 0)
                {
                    matrixOfPossibilities[positionToCheck.Row, positionToCheck.Column] = true;
                }

                positionToCheck.DefineValues(Position.Row - 1, Position.Column - 1);
                if (Board.IsPositionValid(positionToCheck) && IsThereAnEnemy(positionToCheck))
                {
                    matrixOfPossibilities[positionToCheck.Row, positionToCheck.Column] = true;
                }

                positionToCheck.DefineValues(Position.Row - 1, Position.Column + 1);
                if (Board.IsPositionValid(positionToCheck) && IsThereAnEnemy(positionToCheck))
                {
                    matrixOfPossibilities[positionToCheck.Row, positionToCheck.Column] = true;
                }
            }
            else
            {
                positionToCheck.DefineValues(Position.Row + 1, Position.Column);
                if (Board.IsPositionValid(positionToCheck) && IsPositionFree(positionToCheck))
                {
                    matrixOfPossibilities[positionToCheck.Row, positionToCheck.Column] = true;
                }

                positionToCheck.DefineValues(Position.Row + 2, Position.Column);
                Position secondPositionToCheck = new Position(Position.Row + 1, Position.Column);
                if (Board.IsPositionValid(secondPositionToCheck) && IsPositionFree(secondPositionToCheck) && Board.IsPositionValid(positionToCheck) && IsPositionFree(positionToCheck) && NumberOfMovements == 0)
                {
                    matrixOfPossibilities[positionToCheck.Row, positionToCheck.Column] = true;
                }

                positionToCheck.DefineValues(Position.Row + 1, Position.Column - 1);
                if (Board.IsPositionValid(positionToCheck) && IsThereAnEnemy(positionToCheck))
                {
                    matrixOfPossibilities[positionToCheck.Row, positionToCheck.Column] = true;
                }

                positionToCheck.DefineValues(Position.Row + 1, Position.Column + 1);
                if (Board.IsPositionValid(positionToCheck) && IsThereAnEnemy(positionToCheck))
                {
                    matrixOfPossibilities[positionToCheck.Row, positionToCheck.Column] = true;
                }
            }

            return matrixOfPossibilities;
        }

        private bool IsThereAnEnemy(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece != null && piece.Color != Color;
        }

        private bool IsPositionFree(Position position)
        {
            return Board.Piece(position) == null;
        }
    }
}
