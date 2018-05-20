using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource voice;
    [SerializeField] AudioSource bgm;
    [SerializeField] AudioSource ambience;
    [SerializeField] AudioSource soundEffect;

    const float defaultVolume = 0.5f;

    public static AudioManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void PlayVoice(AudioClip clip, float volume)
    {
        if (volume <= 0) volume = defaultVolume;
        voice.clip = clip;
        voice.volume = volume;
        voice.Play();
    }

    public void StopVoice()
    {
        voice.Stop();
    }

    public void PlayBgm(AudioClip clip, float volume, float startTime)
    {
        if (volume <= 0) volume = defaultVolume;
        bgm.clip = clip;
        bgm.volume = volume;
        bgm.time = startTime;
        bgm.Play();
    }

    public void StopBgm()
    {
        bgm.Stop();
    }

    public void PlayAmbience(AudioClip clip, float volume)
    {
        if (volume <= 0) volume = defaultVolume;
        ambience.clip = clip;
        ambience.volume = volume;
        ambience.Play();
    }

    public void StopAmbience()
    {
        ambience.Stop();
    }

    public void PlaySoundEffect(AudioClip clip, float volume)
    {
        if (volume <= 0) volume = defaultVolume;
        soundEffect.clip = clip;
        soundEffect.volume = volume;
        soundEffect.Play();
    }

    public void StopSoundEffect()
    {
        soundEffect.Stop();
    }

    public void StopAllSound()
    {
        bgm.Stop();
        ambience.Stop();
        soundEffect.Stop();
    }
}
