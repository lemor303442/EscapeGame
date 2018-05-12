public class EscapeScene
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string ImagePath { get; private set; }

    public EscapeScene(int _id, string _name, string _imagePath)
    {
        Id = _id;
        Name = _name;
        ImagePath = _imagePath;
    }
}
