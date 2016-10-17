using System.Collections.Generic;

namespace Game
{
    public class NewGenerationEventArgs
    {
        public NewGenerationEventArgs(IList<Cell> changedCells)
        {
            ChangedCells = changedCells;
        }
        public IList<Cell> ChangedCells { get; private set; }
    }
}
