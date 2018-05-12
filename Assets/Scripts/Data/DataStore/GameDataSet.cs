using System.Collections.Generic;

public class GameDataSet
{
    public List<UserItem> UserItemList { get; private set; }
    public List<UserParameter> UserParameterList { get; private set; }

    public GameDataSet()
    {
        UserItemList = new List<UserItem>();
        UserParameterList = new List<UserParameter>();
    }
}

public enum GameDataType
{
    USER_ITEM,
    USER_PARAMETER
}