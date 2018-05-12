public class Item
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string ImagePath { get; private set; }
    public bool InitialIsOwned { get; private set; }

    public Item(int _id, string _name, string _description, string _imagePath, bool _isOwned)
    {
        Id = _id;
        Name = _name;
        Description = _description;
        ImagePath = _imagePath;
        InitialIsOwned = _isOwned;
    }
}
