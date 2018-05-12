public class Layer
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string LayerType { get; private set; }
    public int PosX { get; private set; }
    public int PosY { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }
    public bool FlipX { get; private set; }
    public bool FlipY { get; private set; }
    public int Order { get; private set; }

    //id name    layerType posX    posY width   height flipX   flipY Order

    public Layer(int _id, string _name, string _layerType, int _posX, int _posY, int _width, int _height, bool _flipX, bool _flipY, int _order)
    {
        Id = _id;
        Name = _name;
        LayerType = _layerType;
        PosX = _posX;
        PosY = _posY;
        Width = _width;
        Height = _height;
        FlipX = _flipX;
        FlipY = _flipY;
        Order = _order;
    }
}
