public class EscapeScene
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string ImagePath { get; private set; }
    public string Right { get; private set; }
    public string Left { get; private set; }
    public string Down { get; private set; }

    public EscapeScene(int _id, string _name, string _imagePath, string _right, string _left, string _down)
    {
        Id = _id;
        Name = _name;
        ImagePath = _imagePath;
        Right = _right;
        Left = _left;
        Down = _down;
    }
}
