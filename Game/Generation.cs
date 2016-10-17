using System.Collections.Generic;

namespace Game
{
    public class Generation
    {
        public Field NewField { get; internal set; }
        public IList<Cell> ChangedCells { get; internal set; }
        public Generation()
        {

        }
        public Generation(Field field, IList<Cell> changedCells)
        {
            NewField = field;
            ChangedCells = changedCells;
        }
    }
}
