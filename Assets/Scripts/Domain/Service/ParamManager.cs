using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class ParamManager : MonoBehaviour
{
    Dictionary<string, char> operators = new Dictionary<string, char>(){
        { "equal", '=' },
        { "plus", '+' },
        { "minus", '-' },
        { "multiply", '*' },
        { "devide", '/' },
    };

    public void UpdateParam(string formula)
    {
        List<string> formulaList = FormatFormula(formula);
        ParameterEntity paramEntity = ParameterEntity.FindByParameterName(formulaList[0]);
        if (formulaList.Count == 3)
        {
            if (paramEntity.Parameter.Type == Parameter.ParamType.BOOL)
                paramEntity.UserParameter.UpdateValue(GetBoolValue(formulaList[2]).ToString());
            if (paramEntity.Parameter.Type == Parameter.ParamType.INT)
                paramEntity.UserParameter.UpdateValue(GetIntValue(formulaList[2]).ToString());
        }
        else if (formulaList.Count == 5)
        {
            if (paramEntity.Parameter.Type == Parameter.ParamType.INT)
            {
                int[] values = new int[2];
                values[0] = GetIntValue(formulaList[2]);
                values[1] = GetIntValue(formulaList[4]);
                paramEntity.UserParameter.UpdateValue(Calculate(values, formulaList[3]).ToString());
            }
            else
            {
                ShowWarning(formula);
            }
        }
        else
        {
            ShowWarning(formula);
        }
        GameDataManager.Instance.SaveData();
    }

    int GetIntValue(string value)
    {
        if (Regex.IsMatch(value, @"^(0|[1-9]\d*)$")) return int.Parse(value);
        else return int.Parse(ParameterEntity.FindByParameterName(value).UserParameter.Value);
    }

    bool GetBoolValue(string value)
    {
        if (Regex.IsMatch(value, @"(?:F(?:ALSE|lase)|(?:flas|tru)e|T(?:RUE|rue))")) return Convert.ToBoolean(value);
        else return Convert.ToBoolean(ParameterEntity.FindByParameterName(value).UserParameter.Value);
    }

    int Calculate(int[] values, string operater)
    {
        switch(operater){
            case "+":
                return values[0] + values[1];
            case "-":
                return values[0] - values[1];
            case "*":
                return values[0] * values[1];
            case "/":
                return values[0] / values[1];
            default:
                throw new System.Exception("something wrong with oparator [" + operater + "].");
        }
    }


    List<string> FormatFormula(string formula)
    {
        formula = formula.Replace(" ", "");
        List<string> strList = new List<string>();
        // 左辺と右辺を分ける
        string leftSide = formula.Split(operators["equal"])[0];
        string rightSide = formula.Split(operators["equal"])[1];
        strList.Add(leftSide);
        strList.Add(operators["equal"].ToString());

        if (rightSide.IndexOf(operators["plus"]) != -1)
        {
            string[] array = rightSide.Split(operators["plus"]);
            strList.Add(array[0]);
            strList.Add(operators["plus"].ToString());
            strList.Add(array[1]);
        }
        else if (rightSide.IndexOf(operators["minus"]) != -1)
        {
            string[] array = rightSide.Split(operators["minus"]);
            strList.Add(array[0]);
            strList.Add(operators["minus"].ToString());
            strList.Add(array[1]);
        }
        else if (rightSide.IndexOf(operators["multiply"]) != -1)
        {
            string[] array = rightSide.Split(operators["multiply"]);
            strList.Add(array[0]);
            strList.Add(operators["multiply"].ToString());
            strList.Add(array[1]);
        }
        else if (rightSide.IndexOf(operators["devide"]) != -1)
        {
            string[] array = rightSide.Split(operators["devide"]);
            strList.Add(array[0]);
            strList.Add(operators["devide"].ToString());
            strList.Add(array[1]);
        }
        else
        {
            strList.Add(rightSide);
        }
        return strList;
    }

    void ShowWarning(string formula)
    {
        Debug.LogWarning("something wrong with Parameter.Arg1 [" + formula + "].");
    }
}
