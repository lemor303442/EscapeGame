public class Hint
{
    public int Id { get; private set; }
    public string JumpTo { get; private set; }
    public int HintIndex { get; private set; }
    public string Conditions { get; private set; }

    public Hint(int _id, string _jumpTo, int _hintIndex, string _conditions)
    {
        Id = _id;
        JumpTo = _jumpTo;
        HintIndex = _hintIndex;
        Conditions = _conditions;
    }
}
