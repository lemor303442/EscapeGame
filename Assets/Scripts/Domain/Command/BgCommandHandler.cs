using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgCommandHandler : ScenarioCommandHandler
{
    public class Options : CommandOptions
    {
        public Layer Layer { get; private set; }
        public Sprite Img { get; private set; }

        Options(Layer layer, Sprite img)
        {
            Layer = layer;
            Img = img;
        }

        public static Options Create(Scenario scenario)
        {
            var layer = LayerRepository.FindByName(scenario.Arg1);
            var image = Resources.Load<Sprite>(scenario.Arg2);
            if (image == null)
            {
                Debug.LogWarningFormat("Bg Error: [{0}] not found", scenario.Arg2);
            }
            return new Options(layer, image);
        }
    }

    public BgCommandHandler(ScenarioManager scenarioManager) : base(scenarioManager)
    {
    }

    public override void OnCommand(CommandOptions commandOptions)
    {
        base.OnCommand(commandOptions);
        OnCommandBg(commandOptions as Options);
    }

    void OnCommandBg(Options options)
    {
        if (options.Img == null) return;
        scenarioManager.ScenarioView.UpdateLayerImage(
            options.Layer, options.Img
        );
    }
}
