using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCommandHandler : ScenarioCommandHandler
{
    public class Options : CommandOptions
    {
        public string DestinationName { get; private set; }
        public string Conditions { get; private set; }

        Options(string destinationName, string conditions)
        {
            DestinationName = destinationName;
            Conditions = conditions;
        }

        public static Options Create(Scenario scenario)
        {
            return new Options(scenario.Arg1, scenario.Arg2);
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
        if (ConditionHelper.IsAllConditionValid(options.Conditions)) scenarioManager.JumpTo(options.DestinationName);
    }
}
