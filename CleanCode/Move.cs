namespace CleanCode
{
    public class Move
    {
        private readonly Board board;
        private readonly Location from;
        private readonly Location to;
        private readonly Cell oldDestinationCell;

        public Move(Board board, Location from, Location to, Cell oldDestinationCell)
        {
            this.board = board;
            this.from = from;
            this.to = to;
            this.oldDestinationCell = oldDestinationCell;
        }

        public void Undo()
        {
            board.Set(from, board.Get(to));
            board.Set(to, oldDestinationCell);
        }
    }
}