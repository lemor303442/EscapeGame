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
    public List<string> Items { get; private set; }
    public List<string> Conditions { get; private set; }

    public EscapeInput(int _id, string _jumpTo, string _sceneName, int _posX, int _posY, int _width, int _height, string _items, string _conditions)
    {
        Id = _id;
        JumpTo = _jumpTo;
        SceneName = _sceneName;
        PosX = _posX;
        PosY = _posY;
        Width = _width;
        Height = _height;
        Items = new List<string>();
        foreach (string item in _items.Replace(" ", "").Split('&'))
        {
            Items.Add(item);
        }
        Conditions = new List<string>();
        foreach (string condition in _conditions.Replace(" ", "").Split('&'))
        {
            Conditions.Add(condition);
        }
    }


    public static string FormatListToString(List<string> list)
    {
        string val = "";
        foreach(string str in list){
            val += str + "&";
        }
        return val.Remove(val.Length - 1);
    }
}
