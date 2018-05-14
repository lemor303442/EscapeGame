using UnityEngine;

public class Character
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Pattern { get; private set; }
    public string Pivot { get; private set; }
    public string FilePath { get; private set; }
    public int PosX { get; private set; }
    public int PosY { get; private set; }

    public Character(int _id, string _name, string _pattern, string _pivot, string _filePath, int _posX, int _posY)
    {
        Validation(_id, _name, _pattern, _pivot, _filePath, _posX, _posY);
        Id = _id;
        Name = _name;
        Pattern = _pattern;
        Pivot = _pivot;
        FilePath = _filePath;
        PosX = _posX;
        PosY = _posY;
    }

    private void Validation(int _id, string _name, string _pattern, string _pivot, string _filePath, int _posX, int _posY)
    {
        if (string.IsNullOrEmpty(_name))
            Debug.LogError("Character Validation Error at [" + _id.ToString() + ".Name]\n" +
                           "Character.Name must not be null.");
        if (string.IsNullOrEmpty(_name))
            Debug.LogError("Character Validation Error at [" + _id.ToString() + ".Pattern]\n" +
                           "Character.Pattern must not be null.");
        if (string.IsNullOrEmpty(_pivot))
            Debug.LogError("Character Validation Error at [" + _id.ToString() + ".Pivot]\n" +
                           "Character.Pivot must not be null.");
        if (string.IsNullOrEmpty(_filePath))
            Debug.LogError("Character Validation Error at [" + _id.ToString() + ".FilePath]\n" +
                           "Character.FilePath must not be null.");
    }
}
