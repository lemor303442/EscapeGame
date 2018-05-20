using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamCommandHandler : ScenarioCommandHandler
{
    public class Options : CommandOptions
    {
        public List<string> FormulaList { get; private set; }
        public ParameterEntity ParameterEntity { get; private set; }

        Options(List<string> formulaList, ParameterEntity parameterEntity)
        {
            FormulaList = formulaList;
            ParameterEntity = parameterEntity;
        }

        public static Options Create(Scenario scenario)
        {
            List<string> formulaList = FormulaHelper.FormatFormula(scenario.Arg1);
            var paramEntity = ParameterEntity.FindByParameterName(formulaList[0]);
            return new Options(formulaList, paramEntity);
        }
    }

    public ParamCommandHandler(ScenarioManager scenarioManager) : base(scenarioManager)
    {
    }

    public override void OnCommand(CommandOptions commandOptions)
    {
        base.OnCommand(commandOptions);
        OnCommandBg(commandOptions as Options);
    }

    void OnCommandBg(Options options)
    {
        if (options.FormulaList.Count == 3)
        {
            if (options.ParameterEntity.Parameter.Type == Parameter.ParamType.BOOL)
                options.ParameterEntity.UserParameter.UpdateValue(FormulaHelper.GetBoolValue(options.FormulaList[2]).ToString());
            if (options.ParameterEntity.Parameter.Type == Parameter.ParamType.INT)
                options.ParameterEntity.UserParameter.UpdateValue(FormulaHelper.GetIntValue(options.FormulaList[2]).ToString());
        }
        else if (options.FormulaList.Count == 5)
        {
            if (options.ParameterEntity.Parameter.Type == Parameter.ParamType.INT)
            {
                int[] values = new int[2];
                values[0] = FormulaHelper.GetIntValue(options.FormulaList[2]);
                values[1] = FormulaHelper.GetIntValue(options.FormulaList[4]);
                options.ParameterEntity.UserParameter.UpdateValue(FormulaHelper.Calculate(values, options.FormulaList[3]).ToString());
            }
            else
            {
                Debug.LogWarning("something wrong with Parameter.Arg1.");
            }
        }
        else
        {
            Debug.LogWarning("something wrong with Parameter.Arg1.");
        }
        GameDataManager.Instance.SaveData();
    }
}
