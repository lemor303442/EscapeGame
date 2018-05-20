using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextCommandHandler : ScenarioCommandHandler
{
    public class Options : CommandOptions
    {
        public string Name { get; private set; }
        public string Content { get; private set; }
        public AudioClip VoiceClip { get; private set; }
        public float VoiceVolume { get; private set; }
        public float TextSpeed { get; private set; }

        Options(string name, string content, AudioClip voiceClip, float voiceVolume, float textSpeed)
        {
            Name = name;
            Content = content;
            VoiceClip = voiceClip;
            VoiceVolume = voiceVolume;
            TextSpeed = textSpeed;
        }

        public static Options Create(Scenario scenario)
        {
            AudioClip clip = Resources.Load<AudioClip>(scenario.Arg2);
            return new Options(
                scenario.Arg1,
                scenario.Text,
                clip,
                string.IsNullOrEmpty(scenario.Arg3) ? 0 : float.Parse(scenario.Arg3),
                string.IsNullOrEmpty(scenario.Arg4) ? 0 : float.Parse(scenario.Arg4)
            );
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
        scenarioManager.ScenarioView.UpdateNameText(options.Name);

        //テキストの表示
        scenarioManager.ScenarioView.UpdateContentText(options.Content, options.TextSpeed);

        //ボイスの再生
        if (options.VoiceClip != null)
        {
                AudioManager.Instance.PlayVoice(options.VoiceClip, options.VoiceVolume);
        }
    }
}
