using System.Collections.Generic;

public class EscapeInput
{
    public int Id { get; private set; }
    public string JumpTo { get; private set; }
    public string SceneName { get; private set; }
    public int PosX { get; private set; }
    public int PosY { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }
    public string Conditions { get; private set; }

    public EscapeInput(int _id, string _jumpTo, string _sceneName, int _posX, int _posY, int _width, int _height, string _conditions)
    {
        Id = _id;
        JumpTo = _jumpTo;
        SceneName = _sceneName;
        PosX = _posX;
        PosY = _posY;
        Width = _width;
        Height = _height;
        Conditions = _conditions;
    }
}
