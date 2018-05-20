using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCommandHandler : ScenarioCommandHandler
{
    public class Options : CommandOptions
    {
        public Layer Layer { get; private set; }
        public Character Character { get; private set; }
        public Sprite Img { get; private set; }


        Options(Layer layer, Character character, Sprite img)
        {
            Layer = layer;
            Character = character;
            Img = img;
        }

        public static Options Create(Scenario scenario)
        {
            var layer = LayerRepository.FindByName(scenario.Arg1);
            var charatcer = CharacterRepository.FindByPattern(scenario.Arg2, scenario.Arg3);
            if (charatcer == null) {
                Debug.LogWarning("Cannot find Character with name = [" + scenario.Arg2 + "], pattern = [" + scenario.Arg3 + "]");
            }
            var image = Resources.Load<Sprite>(charatcer.FilePath);
            if (image == null)
            {
                Debug.LogWarningFormat("Character Error: [{0}] not found", charatcer.FilePath);
            }
            return new Options(layer, charatcer, image);
        }
    }

    public CharacterCommandHandler(ScenarioManager scenarioManager) : base(scenarioManager)
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


        if (options.Img == null) return;
        // Pivotを調整
        switch (options.Character.Pivot)
        {
            case "Bottom":
                scenarioManager.ScenarioView.UpdateLayerPivot(options.Layer, TextAnchor.LowerCenter);
                break;
            case "Center":
                scenarioManager.ScenarioView.UpdateLayerPivot(options.Layer, TextAnchor.MiddleCenter);
                break;
            case "Top":
                scenarioManager.ScenarioView.UpdateLayerPivot(options.Layer, TextAnchor.UpperCenter);
                break;
            default:
                Debug.LogWarning("Unkown Character.Pivot [" + options.Character.Pivot + "].");
                break;
        }
        //paddingを調整
        scenarioManager.ScenarioView.UpdatePadding(options.Layer, options.Character);
        //Spriteを表示
        scenarioManager.ScenarioView.UpdateLayerImage(options.Layer, options.Img);
        // Imageのサイズを調整
        float imageRatio = options.Img.rect.height / options.Img.rect.width;
        Vector2 size = new Vector2(options.Layer.Width, options.Layer.Width * imageRatio);
        if (size.y > options.Layer.Height) size = new Vector2(size.x, options.Layer.Height);
        scenarioManager.ScenarioView.UpdateLayerImageSize(options.Layer, size);
    }
}
