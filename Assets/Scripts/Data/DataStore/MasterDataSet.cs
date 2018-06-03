using System.Collections.Generic;

public class MasterDataSet
{
    public List<Character> CharacterList { get; private set; }
    public List<EscapeInput> EscapeInputList { get; private set; }
    public List<EscapeScene> EscapeSceneList { get; private set; }
    public List<Hint> HintList { get; private set; }
    public List<Item> ItemList { get; private set; }
    public List<Layer> LayerList { get; private set; }
    public List<Parameter> ParameterList { get; private set; }
    public List<Scenario> ScenarioList { get; private set; }

    public MasterDataSet()
    {
        CharacterList = new List<Character>();
        EscapeInputList = new List<EscapeInput>();
        EscapeSceneList = new List<EscapeScene>();
        HintList = new List<Hint>();
        ItemList = new List<Item>();
        LayerList = new List<Layer>();
        ParameterList = new List<Parameter>();
        ScenarioList = new List<Scenario>();
    }
}

public enum DataType
{
    CHARACTER,
    ESCAPE_INPUT,
    ESCAPE_SCENE,
    HINT,
    ITEM,
    LAYER,
    LOG,
    PARAMETER,
    SCENARIO
}