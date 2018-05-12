using System.Collections.Generic;
using System.Reflection;
using System;


public static class CsvHelper
{
    public static string PropertyListToCsv(Type type)
    {
        string str = "";
        MemberInfo[] members = type.GetProperties();
        foreach (MemberInfo member in members)
        {
            str += member.Name + ",";
        }
        return str.Remove(str.Length - 1);
    }

    public static List<string[]> FormatCsvToStringArrayList(string csvString)
    {
        List<string[]> stringArrayList = new List<string[]>();
        string[] rows = csvString.Replace("\r\n", "\n").Split('\n');
        foreach (string row in rows)
        {
            stringArrayList.Add(row.Split(','));
        }
        return stringArrayList;
    }

    public static string FormatListToCsv(List<Character> list)
    {
        string csvSring = "";
        csvSring += PropertyListToCsv(typeof(Character)) + "\n";
        foreach (Character item in list)
        {
            csvSring += item.Id.ToString() + ","
                            + item.Name + ","
                            + item.Pattern + ","
                            + item.Pivot + ","
                            + item.FilePath + ","
                            + item.PosX.ToString() + ","
                            + item.PosY.ToString();
            csvSring += "\n";
        }
        return csvSring.Remove(csvSring.Length - 1);
    }

    public static string FormatListToCsv(List<EscapeInput> list)
    {
        string csvSring = "";
        csvSring += PropertyListToCsv(typeof(EscapeInput)) + "\n";
        foreach (EscapeInput item in list)
        {
            csvSring += item.Id.ToString() + ","
                            + item.JumpTo + ","
                            + item.SceneName + ","
                            + item.PosX.ToString() + ","
                            + item.PosY.ToString() + ","
                            + item.Width.ToString() + ","
                            + item.Height.ToString() + ","
                            + EscapeInput.FormatListToString(item.Items) + ","
                            + EscapeInput.FormatListToString(item.Conditions);
            csvSring += "\n";
        }
        return csvSring.Remove(csvSring.Length - 1);
    }

    public static string FormatListToCsv(List<EscapeScene> list)
    {
        string csvSring = "";
        csvSring += PropertyListToCsv(typeof(EscapeScene)) + "\n";
        foreach (EscapeScene item in list)
        {
            csvSring += item.Id.ToString() + ","
                            + item.Name + ","
                            + item.ImagePath;
            csvSring += "\n";
        }
        return csvSring.Remove(csvSring.Length - 1);
    }

    public static string FormatListToCsv(List<Item> list)
    {
        string csvSring = "";
        csvSring += PropertyListToCsv(typeof(Item)) + "\n";
        foreach (Item item in list)
        {
            csvSring += item.Id.ToString() + ","
                            + item.Name + ","
                            + item.Description + ","
                            + item.ImagePath + ","
                            + item.InitialIsOwned.ToString();
            csvSring += "\n";
        }
        return csvSring.Remove(csvSring.Length - 1);
    }

    public static string FormatListToCsv(List<Layer> list)
    {
        string csvSring = "";
        csvSring += PropertyListToCsv(typeof(Layer)) + "\n";
        foreach (Layer layer in list)
        {
            csvSring += layer.Id.ToString() + ','
                             + layer.Name + ","
                             + layer.LayerType + ","
                             + layer.PosX.ToString() + ","
                             + layer.PosY.ToString() + ","
                             + layer.Width.ToString() + ","
                             + layer.Height.ToString() + ","
                             + layer.FlipX.ToString() + ","
                             + layer.FlipY.ToString() + ","
                             + layer.Order.ToString();
            csvSring += "\n";
        }
        return csvSring.Remove(csvSring.Length - 1);
    }

    public static string FormatListToCsv(List<Parameter> list)
    {
        string csvSring = "";
        csvSring += PropertyListToCsv(typeof(Parameter)) + "\n";
        foreach (Parameter param in list)
        {
            csvSring += param.Id.ToString() + ","
                             + param.Name + ","
                             + param.Type.ToString() + ","
                             + param.InitialValue;
            csvSring += "\n";
        }
        return csvSring.Remove(csvSring.Length - 1);
    }

    public static string FormatListToCsv(List<Scenario> list)
    {
        string csvSring = "";
        csvSring += PropertyListToCsv(typeof(Scenario)) + "\n";
        foreach (Scenario param in list)
        {
            csvSring += param.Id.ToString() + ","
                             + param.Command + ","
                             + param.Arg1 + ","
                             + param.Arg2 + ","
                             + param.Arg3 + ","
                             + param.Arg4 + ","
                             + param.Arg5 + ","
                             + param.Arg6 + ","
                             + param.Text;
            csvSring += "\n";
        }
        return csvSring.Remove(csvSring.Length - 1);
    }

    public static string FormatListToCsv(List<UserItem> list)
    {
        string csvSring = "";
        csvSring += PropertyListToCsv(typeof(UserItem)) + "\n";
        foreach (UserItem param in list)
        {
            csvSring += param.Id.ToString() + ","
                             + param.ItemId.ToString() + ","
                             + param.IsOwned.ToString();
            csvSring += "\n";
        }
        return csvSring.Remove(csvSring.Length - 1);
    }

    public static string FormatListToCsv(List<UserParameter> list)
    {
        string csvSring = "";
        csvSring += PropertyListToCsv(typeof(UserParameter)) + "\n";
        foreach (UserParameter param in list)
        {
            csvSring += param.Id.ToString() + ","
                             + param.ParameterId.ToString() + ","
                             + param.Value;
            csvSring += "\n";
        }
        return csvSring.Remove(csvSring.Length - 1);
    }
}
