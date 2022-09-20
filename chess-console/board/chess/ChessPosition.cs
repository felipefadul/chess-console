namespace board.chess
{
    internal class ChessPosition
    {

        public char Column { get; set; }
        public int Row { get; set; }
        private const int MAXIMUM_NUMBER_OF_ROWS_POSITIONS = 8;

        public ChessPosition(char column, int row)
        {
            Column = column;
            Row = row;
        }

        public override string ToString()
        {
            return "" + Column.ToString().ToUpper() + Row;
        }

        public Position ToPosition()
        {
            return new Position(MAXIMUM_NUMBER_OF_ROWS_POSITIONS - Row, Column - 'a');
        }
    }
}
