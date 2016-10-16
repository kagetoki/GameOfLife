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
    }
}
