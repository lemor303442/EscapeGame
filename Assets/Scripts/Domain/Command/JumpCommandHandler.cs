using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCommandHandler : ScenarioCommandHandler
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

    public JumpCommandHandler(ScenarioManager scenarioManager) : base(scenarioManager)
    {
    }

    public override void OnCommand(CommandOptions commandOptions)
    {
        base.OnCommand(commandOptions);
        OnCommandBg(commandOptions as Options);
    }

    void OnCommandBg(Options options)
    {
        if (ConditionHelper.IsAllConditionValid(options.Scenario.Arg2)) scenarioManager.JumpTo(options.Scenario.Arg1);
    }
}
