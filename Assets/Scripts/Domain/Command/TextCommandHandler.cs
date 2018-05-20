using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextCommandHandler : ScenarioCommandHandler
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

    public TextCommandHandler(ScenarioManager scenarioManager) : base(scenarioManager)
    {
    }

    public override void OnCommand(CommandOptions commandOptions)
    {
        base.OnCommand(commandOptions);
        OnCommandBg(commandOptions as Options);
    }

    void OnCommandBg(Options options)
    {
        //名前の表示
        scenarioManager.ScenarioView.UpdateNameText(options.Scenario.Arg1);

        //テキストの表示
        if (string.IsNullOrEmpty(options.Scenario.Arg4))
            scenarioManager.ScenarioView.UpdateContentText(options.Scenario.Text);
        else
            scenarioManager.ScenarioView.UpdateContentText(options.Scenario.Text, float.Parse(options.Scenario.Arg4));
        
        //ボイスの再生
        if (!string.IsNullOrEmpty(options.Scenario.Arg2))
        {
            if (string.IsNullOrEmpty(options.Scenario.Arg3))
                AudioManager.Instance.PlayVoice(options.Scenario.Arg2);
            else
                AudioManager.Instance.PlayVoice(options.Scenario.Arg2, float.Parse(options.Scenario.Arg3));
        }
    }
}
