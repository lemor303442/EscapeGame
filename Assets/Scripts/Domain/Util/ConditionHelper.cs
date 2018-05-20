using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class ConditionHelper
{
    
    public static bool IsAllConditionValid(string formula){
        List<string> conditionList = ConditionHelper.GetConditions(formula);
        if (conditionList == null) return true;
        foreach (string condition in conditionList)
        {
            if (!IsConditionValid(condition))
            {
                return false;
            }
        }
        return true;
    }

    private static bool IsConditionValid(string formula)
    {
        formula = formula.Replace(" ", "");
        List<string> formulaList = FormulaHelper.FormatFormula(formula);
        if (formulaList.Count == 1)
        {
            // アイテムの場合
            bool isReverse = Regex.IsMatch(formulaList[0], @"^\!");
            string itemName = formulaList[0];
            if (isReverse) itemName = itemName.Substring(1);
            ItemEntity itemEntity = ItemEntity.FindByItemName(itemName);
            if (isReverse) return !itemEntity.UserItem.IsOwned;
            else return itemEntity.UserItem.IsOwned;
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

    private static List<string> GetConditions(string condition)
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
