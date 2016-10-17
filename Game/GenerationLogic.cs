using System.Collections.Generic;

namespace Game
{
    public class GenerationLogic
    {
        public Field NewGeneration(Field field)
        {
            var result = new Field(field.Width, field.Height);

            for (int i = 0; i < field.Width; i++)
            {
                for (int j = 0; j < field.Height; j++)
                {
                    var neighboursCount = field.NeighboursCountOf(i, j);
                    if(neighboursCount > 3 || neighboursCount < 2)
                    {
                        result[i, j].IsAlive = false;
                        continue;
                    }
                    if((neighboursCount == 2 && field[i,j].IsAlive) || neighboursCount == 3)
                    {
                        result[i, j].IsAlive = true;
                    }
                }
            }

            return result;
        }

        public List<Cell> ChangedCells(Field field)
        {
            var result = new List<Cell>();
            for (int i = 0; i < field.Width; i++)
            {
                for (int j = 0; j < field.Height; j++)
                {
                    var neighboursCount = field.NeighboursCountOf(i, j);
                    if (neighboursCount > 3 || neighboursCount < 2)//dead
                    {
                        if (field[i, j].IsAlive)
                        {
                            result.Add(new Cell(i, j) { IsAlive = false });
                        }
                        continue;
                    }
                    if ((neighboursCount == 2 && field[i, j].IsAlive) || neighboursCount == 3)//lives/borns
                    {
                        if(!field[i, j].IsAlive)
                        {
                            result.Add(new Cell(i, j) { IsAlive = true });
                        }
                    }
                }
            }
            return result;
        }

        public Generation Generation(Field field)
        {
            var result = new Generation(new Field(field.Width, field.Height), new List<Cell>());
            for (int i = 0; i < field.Width; i++)
            {
                for (int j = 0; j < field.Height; j++)
                {
                    var neighboursCount = field.NeighboursCountOf(i, j);
                    if (neighboursCount > 3 || neighboursCount < 2)//dead
                    {
                        if (field[i, j].IsAlive)
                        {
                            result.ChangedCells.Add(new Cell(i, j) { IsAlive = false });
                        }
                        result.NewField[i, j].IsAlive = false;
                        continue;
                    }
                    if ((neighboursCount == 2 && field[i, j].IsAlive) || neighboursCount == 3)//lives/borns
                    {
                        if (!field[i, j].IsAlive)
                        {
                            result.ChangedCells.Add(new Cell(i, j) { IsAlive = true });
                        }
                        result.NewField[i, j].IsAlive = true;
                    }
                }
            }
            return result;
        }
    }
}
