using System;
using System.Collections.Generic;

public class MasterDataLoader
{
    public MasterDataSet ReadData()
    {
        MasterDataSet dataSet = new MasterDataSet();
        AddList(dataSet, DataType.CHARACTER, TextFileHelper.Read(Const.Path.MasterData.character));
        AddList(dataSet, DataType.ESCAPE_INPUT, TextFileHelper.Read(Const.Path.MasterData.escapeInput));
        AddList(dataSet, DataType.ESCAPE_SCENE, TextFileHelper.Read(Const.Path.MasterData.escapeScene));
        AddList(dataSet, DataType.HINT, TextFileHelper.Read(Const.Path.MasterData.hint));
        AddList(dataSet, DataType.ITEM, TextFileHelper.Read(Const.Path.MasterData.item));
        AddList(dataSet, DataType.LAYER, TextFileHelper.Read(Const.Path.MasterData.layer));
        AddList(dataSet, DataType.PARAMETER, TextFileHelper.Read(Const.Path.MasterData.parameter));
        AddList(dataSet, DataType.SCENARIO, TextFileHelper.Read(Const.Path.MasterData.scenario));
        return dataSet;
    }

    public void DownloadData()
    {
        MasterDataSet dataSet = new MasterDataSet();
        Action<DataType, string> OnDownloadComplete = (dataType, response) =>
        {
            AddList(dataSet, dataType, response);
            SaveData(dataSet, dataType);
        };
        DownloadHelper.CallDownloadCsv(DataType.CHARACTER, Const.Url.character, OnDownloadComplete);
        DownloadHelper.CallDownloadCsv(DataType.ESCAPE_INPUT, Const.Url.escapeInput, OnDownloadComplete);
        DownloadHelper.CallDownloadCsv(DataType.ESCAPE_SCENE, Const.Url.escapeScene, OnDownloadComplete);
        DownloadHelper.CallDownloadCsv(DataType.HINT, Const.Url.hint, OnDownloadComplete);
        DownloadHelper.CallDownloadCsv(DataType.ITEM, Const.Url.item, OnDownloadComplete);
        DownloadHelper.CallDownloadCsv(DataType.LAYER, Const.Url.layer, OnDownloadComplete);
        DownloadHelper.CallDownloadCsv(DataType.PARAMETER, Const.Url.parameter, OnDownloadComplete);
        DownloadHelper.CallDownloadCsv(DataType.SCENARIO, Const.Url.scenario, OnDownloadComplete);
    }

    private void AddList(MasterDataSet dataSet, DataType dataType, string csvString)
    {
        List<string[]> csvDataList = CsvHelper.FormatCsvToStringArrayList(csvString);
        switch (dataType)
        {
            case DataType.CHARACTER:
                foreach (string[] data in csvDataList)
                {
                    if (data == csvDataList[0]) continue;
                    dataSet.CharacterList.Add(new Character(int.Parse(data[0]), data[1], data[2], data[3], data[4], int.Parse(data[5]), int.Parse(data[6])));
                }
                break;
            case DataType.ESCAPE_INPUT:
                foreach (string[] data in csvDataList)
                {
                    if (data == csvDataList[0]) continue;
                    dataSet.EscapeInputList.Add(new EscapeInput(int.Parse(data[0]), data[1], data[2], int.Parse(data[3]), int.Parse(data[4]), int.Parse(data[5]), int.Parse(data[6]), data[7]));
                }
                break;
            case DataType.ESCAPE_SCENE:
                foreach (string[] data in csvDataList)
                {
                    if (data == csvDataList[0]) continue;
                    dataSet.EscapeSceneList.Add(new EscapeScene(int.Parse(data[0]), data[1], data[2], data[3], data[4], data[5]));
                }
                break;
            case DataType.HINT:
                foreach (string[] data in csvDataList)
                {
                    if (data == csvDataList[0]) continue;
                    dataSet.HintList.Add(new Hint(int.Parse(data[0]), data[1], int.Parse(data[2]), data[3]));
                }
                break;
            case DataType.ITEM:
                foreach (string[] data in csvDataList)
                {
                    if (data == csvDataList[0]) continue;
                    dataSet.ItemList.Add(new Item(int.Parse(data[0]), data[1], data[2], data[3], Convert.ToBoolean(data[4])));
                }
                break;
            case DataType.LAYER:
                foreach (string[] data in csvDataList)
                {
                    if (data == csvDataList[0]) continue;
                    dataSet.LayerList.Add(new Layer(
                        int.Parse(data[0]),
                        data[1],
                        data[2],
                        int.Parse(data[3]),
                        int.Parse(data[4]),
                        int.Parse(data[5]),
                        int.Parse(data[6]),
                        Convert.ToBoolean(data[7]),
                        Convert.ToBoolean(data[8]),
                        int.Parse(data[9])
                    ));
                }
                break;
            case DataType.PARAMETER:
                foreach (string[] data in csvDataList)
                {
                    if (data == csvDataList[0]) continue;
                    dataSet.ParameterList.Add(new Parameter(int.Parse(data[0]), data[1], data[2], data[3]));
                }
                break;
            case DataType.SCENARIO:
                foreach (string[] data in csvDataList)
                {
                    if (data == csvDataList[0]) continue;
                    dataSet.ScenarioList.Add(new Scenario(int.Parse(data[0]), data[1], data[2], data[3], data[4], data[5], data[6], data[7], data[8]));
                }
                break;
            default:
                break;
        }
    }

    private void SaveData(MasterDataSet dataSet, DataType type)
    {
        switch (type)
        {
            case DataType.CHARACTER:
                TextFileHelper.Write(Const.Path.MasterData.character, CsvHelper.FormatListToCsv(dataSet.CharacterList));
                break;
            case DataType.ESCAPE_INPUT:
                TextFileHelper.Write(Const.Path.MasterData.escapeInput, CsvHelper.FormatListToCsv(dataSet.EscapeInputList));
                break;
            case DataType.ESCAPE_SCENE:
                TextFileHelper.Write(Const.Path.MasterData.escapeScene, CsvHelper.FormatListToCsv(dataSet.EscapeSceneList));
                break;
            case DataType.HINT:
                TextFileHelper.Write(Const.Path.MasterData.hint, CsvHelper.FormatListToCsv(dataSet.HintList));
                break;
            case DataType.ITEM:
                TextFileHelper.Write(Const.Path.MasterData.item, CsvHelper.FormatListToCsv(dataSet.ItemList));
                break;
            case DataType.LAYER:
                TextFileHelper.Write(Const.Path.MasterData.layer, CsvHelper.FormatListToCsv(dataSet.LayerList));
                break;
            case DataType.PARAMETER:
                TextFileHelper.Write(Const.Path.MasterData.parameter, CsvHelper.FormatListToCsv(dataSet.ParameterList));
                break;
            case DataType.SCENARIO:
                TextFileHelper.Write(Const.Path.MasterData.scenario, CsvHelper.FormatListToCsv(dataSet.ScenarioList));
                break;
            default:
                break;
        }
    }
}
