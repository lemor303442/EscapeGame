﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectCommandHandler : ScenarioCommandHandler
{
    public class Options : CommandOptions
    {
        public AudioClip AudioClip { get; private set; }
        public float Volume { get; private set; }

        Options(AudioClip audioClip, float volume)
        {
            AudioClip = audioClip;
            Volume = volume;
        }

        public static Options Create(Scenario scenario)
        {
            AudioClip clip = Resources.Load<AudioClip>(scenario.Arg1);
            if (clip == null)
            {
                Debug.LogWarningFormat("SoundEffect Error: [{0}] not found", scenario.Arg1);
            }
            return new Options(
                clip,
                string.IsNullOrEmpty(scenario.Arg2) ? 0 : float.Parse(scenario.Arg2)
            );
        }
    }

    public SoundEffectCommandHandler(ScenarioManager scenarioManager) : base(scenarioManager)
    {
    }

    public override void OnCommand(CommandOptions commandOptions)
    {
        base.OnCommand(commandOptions);
        OnCommandBg(commandOptions as Options);
    }

    void OnCommandBg(Options options)
    {
        AudioManager.Instance.PlaySoundEffect(options.AudioClip, options.Volume);
    }
}
