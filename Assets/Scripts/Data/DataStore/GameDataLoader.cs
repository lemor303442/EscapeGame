using System;
using System.Collections.Generic;
using UnityEngine;

public class GameDataLoader
{
    public GameDataSet CreateInitialData()
    {
        GameDataSet dataSet = new GameDataSet();
        foreach (Item item in MasterDataManager.Instance.MasterDataSet.ItemList)
        {
            dataSet.UserItemList.Add(new UserItem(dataSet.UserItemList.Count, item.Id, item.InitialIsOwned));
        }
        foreach (Parameter param in MasterDataManager.Instance.MasterDataSet.ParameterList)
        {
            dataSet.UserParameterList.Add(new UserParameter(dataSet.UserParameterList.Count, param.Id, param.InitialValue));
        }
        SaveData(dataSet);
        return dataSet;
    }

    public GameDataSet ReadData()
    {
        GameDataSet dataSet = new GameDataSet();
        AddList(dataSet, GameDataType.USER_ITEM, TextFileHelper.Read(Const.Path.GameData.userItem));
        AddList(dataSet, GameDataType.USER_PARAMETER, TextFileHelper.Read(Const.Path.GameData.userParameter));
        return dataSet;
    }

    private void AddList(GameDataSet dataSet, GameDataType dataType, string csvString)
    {
        List<string[]> csvDataList = CsvHelper.FormatCsvToStringArrayList(csvString);
        switch (dataType)
        {
            case GameDataType.USER_ITEM:
                foreach (string[] data in csvDataList)
                {
                    if (data == csvDataList[0]) continue;
                    dataSet.UserItemList.Add(new UserItem(int.Parse(data[0]), int.Parse(data[1]), Convert.ToBoolean(data[2])));
                }
                break;
            case GameDataType.USER_PARAMETER:
                foreach (string[] data in csvDataList)
                {
                    if (data == csvDataList[0]) continue;
                    dataSet.UserParameterList.Add(new UserParameter(int.Parse(data[0]), int.Parse(data[1]), data[2]));
                }
                break;
            default:
                break;
        }
    }

    public static void SaveData(GameDataSet dataSet)
    {
        TextFileHelper.Write(Const.Path.GameData.userItem, CsvHelper.FormatListToCsv(dataSet.UserItemList));
        TextFileHelper.Write(Const.Path.GameData.userParameter, CsvHelper.FormatListToCsv(dataSet.UserParameterList));
    }
}
