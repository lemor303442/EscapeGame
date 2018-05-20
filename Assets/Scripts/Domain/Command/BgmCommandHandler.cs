using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmCommandHandler : ScenarioCommandHandler
{
    public class Options : CommandOptions
    {
        public AudioClip VoiceClip { get; private set; }
        public float VoiceVolume { get; private set; }
        public float StartTime { get; private set; }

        Options(AudioClip voiceClip, float voiceVolume, float startTime)
        {
            VoiceClip = voiceClip;
            VoiceVolume = voiceVolume;
            StartTime = startTime;
        }

        public static Options Create(Scenario scenario)
        {
            AudioClip clip = Resources.Load<AudioClip>(scenario.Arg1);
            if (clip == null)
            {
                Debug.LogWarningFormat("Bgm Error: [{0}] not found", scenario.Arg1);
            }
            return new Options(
                clip,
                string.IsNullOrEmpty(scenario.Arg2) ? 0 : float.Parse(scenario.Arg2),
                string.IsNullOrEmpty(scenario.Arg3) ? 0 : float.Parse(scenario.Arg3)
            );
        }
    }

    public BgmCommandHandler(ScenarioManager scenarioManager) : base(scenarioManager)
    {
    }

    public override void OnCommand(CommandOptions commandOptions)
    {
        base.OnCommand(commandOptions);
        OnCommandBg(commandOptions as Options);
    }

    void OnCommandBg(Options options)
    {
        AudioManager.Instance.PlayBgm(options.VoiceClip, options.VoiceVolume, options.StartTime);
    }
}
