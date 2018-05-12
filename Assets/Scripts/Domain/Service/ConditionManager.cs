using System;
using System.Collections.Generic;
using UnityEngine;

public class ConditionManager : MonoBehaviour
{
    public bool IsConditionValid(string formula)
    {
        formula = formula.Replace(" ", "");
        List<string> formulaList = FormulaHelper.FormatFormula(formula);
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
