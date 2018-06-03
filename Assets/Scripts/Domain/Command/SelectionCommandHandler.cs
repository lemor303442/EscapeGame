using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionCommandHandler : ScenarioCommandHandler
{
    public class Options : CommandOptions
    {
        public Scenario Scenario { get; private set; }

        Options(Scenario scenario)
        {
            Scenario = scenario;
        }

        public static Options Create(Scenario scenario)
        {
            return new Options(scenario);
        }
    }

    public SelectionCommandHandler(ScenarioManager scenarioManager) : base(scenarioManager)
    {
    }

    public override void OnCommand(CommandOptions commandOptions)
    {
        base.OnCommand(commandOptions);
        OnCommandBg(commandOptions as Options);
    }

    void OnCommandBg(Options options)
    {
        List<Scenario> selectionList = ScenarioRepository.GetSelections(options.Scenario.Id);
        // 条件を満たしていないselectionを削除
        List<int> removeSelectionIds = new List<int>();
        foreach (Scenario selection in selectionList)
        {
            if (!ConditionHelper.IsAllConditionValid(selection.Arg2))
            {
                removeSelectionIds.Add(selection.Id);
            }
        }
        foreach (int i in removeSelectionIds)
        {
            selectionList.RemoveAll(x => x.Id == i);
        }
        // Selectionを表示
        scenarioManager.ScenarioView.ShowSelections(selectionList);
    }
}
