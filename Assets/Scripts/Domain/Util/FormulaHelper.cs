using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class FormulaHelper
{

    public static List<string> FormatFormula(string formula)
    {
        formula = formula.Replace(" ", "");
        List<string> strList = new List<string>();

        string leftSide = "";
        string rightSide = "";
        if (formula.IndexOf(Const.Operator.equal) != -1)
        {
            leftSide = formula.Split(Const.Operator.equal)[0];
            rightSide = formula.Split(Const.Operator.equal)[1];
            strList.Add(leftSide);
            strList.Add(Const.Operator.equal.ToString());
        }
        else if (formula.IndexOf(Const.Operator.lessThan) != -1)
        {
            leftSide = formula.Split(Const.Operator.lessThan)[0];
            rightSide = formula.Split(Const.Operator.lessThan)[1];
            strList.Add(leftSide);
            strList.Add(Const.Operator.lessThan.ToString());
        }
        else if (formula.IndexOf(Const.Operator.greaterThan) != -1)
        {
            leftSide = formula.Split(Const.Operator.greaterThan)[0];
            rightSide = formula.Split(Const.Operator.greaterThan)[1];
            strList.Add(leftSide);
            strList.Add(Const.Operator.greaterThan.ToString());
        }
        else
        {
            strList.Add(formula);
            return strList;
        }
        if (rightSide.IndexOf(Const.Operator.plus) != -1)
        {
            string[] array = rightSide.Split(Const.Operator.plus);
            strList.Add(array[0]);
            strList.Add(Const.Operator.plus.ToString());
            strList.Add(array[1]);
        }
        else if (rightSide.IndexOf(Const.Operator.minus) != -1)
        {
            string[] array = rightSide.Split(Const.Operator.minus);
            strList.Add(array[0]);
            strList.Add(Const.Operator.minus.ToString());
            strList.Add(array[1]);
        }
        else if (rightSide.IndexOf(Const.Operator.multiply) != -1)
        {
            string[] array = rightSide.Split(Const.Operator.multiply);
            strList.Add(array[0]);
            strList.Add(Const.Operator.multiply.ToString());
            strList.Add(array[1]);
        }
        else if (rightSide.IndexOf(Const.Operator.devide) != -1)
        {
            string[] array = rightSide.Split(Const.Operator.devide);
            strList.Add(array[0]);
            strList.Add(Const.Operator.devide.ToString());
            strList.Add(array[1]);
        }
        else
        {
            strList.Add(rightSide);
        }
        return strList;
    }

    public static int Calculate(int[] values, string operater)
    {
        switch (operater)
        {
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

    public static int GetIntValue(string value)
    {
        if (Regex.IsMatch(value, @"^(0|[1-9]\d*)$")) return int.Parse(value);
        else return int.Parse(ParameterEntity.FindByParameterName(value).UserParameter.Value);
    }

    public static bool GetBoolValue(string value)
    {
        if (Regex.IsMatch(value, @"(?:F(?:ALSE|alse)|(?:fals|tru)e|T(?:RUE|rue))")) return Convert.ToBoolean(value);
        else return Convert.ToBoolean(ParameterEntity.FindByParameterName(value).UserParameter.Value);
    }
}
