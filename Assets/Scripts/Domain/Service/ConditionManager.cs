using System.Collections.Generic;
using UnityEngine;

public class ConditionManager : MonoBehaviour
{
    public bool IsConditionValid(string formula)
    {
        List<string> formulaList = FormulaHelper.FormatFormula(formula);

        return true;
    }
}
