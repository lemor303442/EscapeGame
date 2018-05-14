using System;
using System.Collections.Generic;
using UnityEngine;

public class ConditionHelper
{
    public static bool IsConditionValid(string formula)
    {
        formula = formula.Replace(" ", "");
        List<string> formulaList = FormulaHelper.FormatFormula(formula);
        if (formulaList.Count == 1)
        {
            // アイテムの場合
            Debug.Log(formulaList[0]);
            ItemEntity itemEntity = ItemEntity.FindByItemName(formulaList[0]);
            return itemEntity.UserItem.IsOwned;
        }
        else
        {
            // 式の場合
            ParameterEntity paramEntity = ParameterEntity.FindByParameterName(formulaList[0]);
            if (paramEntity.Parameter.Type == Parameter.ParamType.BOOL)
            {
                return FormulaHelper.GetBoolValue(formulaList[0]) == FormulaHelper.GetBoolValue(formulaList[2]);
            }
            else
            {
                switch (formulaList[1])
                {
                    case "=":
                        return FormulaHelper.GetIntValue(formulaList[0]) == FormulaHelper.GetIntValue(formulaList[2]);
                    case "<":
                        return FormulaHelper.GetIntValue(formulaList[0]) < FormulaHelper.GetIntValue(formulaList[2]);
                    case ">":
                        return FormulaHelper.GetIntValue(formulaList[0]) > FormulaHelper.GetIntValue(formulaList[2]);
                    default:
                        return false;
                }
            }
        }
    }

    public static List<string> GetConditions(string condition)
    {
        if (string.IsNullOrEmpty(condition)) return null;
        condition = condition.Replace(" ", "");
        string[] array = condition.Split('&');
        List<string> list = new List<string>();
        foreach (var str in array)
        {
            list.Add(str);
        }
        return list;
    }
}
