namespace board
{
    internal abstract class Piece
    {
        public Position? Position { get; set; }
        public Color Color { get; protected set; }
        public int NumberOfMovements { get; protected set; }
        public Board Board { get; protected set; }

        public Piece(Board board, Color color)
        {
            Position = null;
            Color = color;
            Board = board;
            NumberOfMovements = 0;
        }

        public void IncreaseNumberOfMovements()
        {
            NumberOfMovements++;
        }

        public void DecreaseNumberOfMovements()
        {
            NumberOfMovements--;
        }

        public bool HasPossibleMovements()
        {
            bool[,] matrixOfPossibilities = PossibleMovements();
            for (int i = 0; i < Board.Rows; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (matrixOfPossibilities[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CanMoveToPosition(Position position)
        {
            return PossibleMovements()[position.Row, position.Column];
        }

        public abstract bool[,] PossibleMovements();
    }
}
