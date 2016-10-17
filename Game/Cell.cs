namespace Game
{
    public class Cell
    {
        public bool IsAlive { get; set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
