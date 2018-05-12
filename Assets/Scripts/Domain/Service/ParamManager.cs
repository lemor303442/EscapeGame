using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class ParamManager : MonoBehaviour
{
    public void UpdateParam(string formula)
    {
        List<string> formulaList = FormulaHelper.FormatFormula(formula);
        ParameterEntity paramEntity = ParameterEntity.FindByParameterName(formulaList[0]);
        if (formulaList.Count == 3)
        {
            if (paramEntity.Parameter.Type == Parameter.ParamType.BOOL)
                paramEntity.UserParameter.UpdateValue(FormulaHelper.GetBoolValue(formulaList[2]).ToString());
            if (paramEntity.Parameter.Type == Parameter.ParamType.INT)
                paramEntity.UserParameter.UpdateValue(FormulaHelper.GetIntValue(formulaList[2]).ToString());
        }
        else if (formulaList.Count == 5)
        {
            if (paramEntity.Parameter.Type == Parameter.ParamType.INT)
            {
                int[] values = new int[2];
                values[0] = FormulaHelper.GetIntValue(formulaList[2]);
                values[1] = FormulaHelper.GetIntValue(formulaList[4]);
                paramEntity.UserParameter.UpdateValue(FormulaHelper.Calculate(values, formulaList[3]).ToString());
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

    void ShowWarning(string formula)
    {
        Debug.LogWarning("something wrong with Parameter.Arg1 [" + formula + "].");
    }
}
