using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game
{
    public class Field
    {
        public int Height { get; private set; }
        public int Width { get; private set; }
        private Cell[][] _field;
        public Field(int width, int height)
        {
            Width = width;
            Height = height;
            _field = new Cell[width][];
            for (int i = 0; i < width; i++)
            {
                _field[i] = new Cell[height];
                for(int j = 0; j< height; j++)
                {
                    _field[i][j] = new Cell();
                }
            }
        }
        public Cell this[int xIndex, int yIndex]
        {
            get { return _field[xIndex][yIndex]; }
            set { _field[xIndex][yIndex] = value; }
        }

        public int NeighboursCountOf(int xIndex, int yIndex)
        {
            if(xIndex < 0 || yIndex < 0 || xIndex > Width - 1 || yIndex > Height - 1) { throw new ArgumentOutOfRangeException(); }
            var result = 0;//new List<bool>(8);
            int xStart = xIndex > 0 ? xIndex - 1 : xIndex;
            int yStart = yIndex > 0 ? yIndex - 1 : yIndex;
            int xFinish = xIndex < Width - 1 ? xIndex + 1 : xIndex;
            int yFinish = yIndex < Height - 1 ? yIndex + 1 : yIndex;
            for (int i = xStart; i <= xFinish; i++)
            {
                for (int j = yStart; j <= yFinish; j++)
                {
                    if(i == xIndex && j == yIndex) { continue; }
                    if (this[i, j].IsAlive)
                    {
                        result++;
                    }
                }
            }
            return result;
        }
    }
}
