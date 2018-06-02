using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteOffCommandHandler : ScenarioCommandHandler
{
    public class Options : CommandOptions
    {
        public Layer Layer { get; private set; }

        Options(Layer layer)
        {
            Layer = layer;
        }

        public static Options Create(Scenario scenario)
        {
            var layer = LayerRepository.FindByName(scenario.Arg1);
            return new Options(layer);
        }
    }

    public SpriteOffCommandHandler(ScenarioManager scenarioManager) : base(scenarioManager)
    {
    }

    public override void OnCommand(CommandOptions commandOptions)
    {
        base.OnCommand(commandOptions);
        OnCommandBg(commandOptions as Options);
    }

    void OnCommandBg(Options options)
    {
        scenarioManager.ScenarioView.UpdateLayerImage(options.Layer, null);
    }
}
