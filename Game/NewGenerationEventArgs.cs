namespace Game
{
    public class NewGenerationEventArgs
    {
        public Field Old { get; private set; }
        public Field New { get; private set; }
        public NewGenerationEventArgs(Field old, Field @new)
        {
            Old = old;
            New = @new;
        }
    }
}
