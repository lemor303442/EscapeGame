using System.Collections.Generic;

public class Parameter {
    public int Id { get; private set; }
    public string Name { get; private set; }
    public ParamType Type { get; private set; }
    public string InitialValue { get; private set; }

    public enum ParamType
    {
        INT,
        BOOL,
        STRING
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Parameter"/> class.
    /// </summary>
    /// <param name="_name">Name.</param>
    /// <param name="_type">"BOOL", "INT"</param>
    /// <param name="_value">Value.</param>
    public Parameter(int _id, string _name, string _type, string _value)
    {
        Id = _id;
        Name = _name;
        Type = GetType(_type);
        InitialValue = _value;
    }

    private ParamType GetType(string _type){
        switch (_type)
        {
            case "BOOL":
                return ParamType.BOOL;
            case "INT":
                return ParamType.INT;
            case "STRING":
                return ParamType.STRING;
            default:
                throw new System.Exception("Paramater.type must be passed by \"BOOL\" or \"INT\"");
        }
    }
}
