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

    public void PlayVoice(string path, float volume = defaultVolume)
    {
        AudioClip clip = Resources.Load<AudioClip>(path);
        if (clip == null)
        {
            Debug.LogWarning("Voice Error: [" + path + "] not found");
            return;
        }
        voice.clip = clip;
        voice.volume = volume;
        voice.Play();
    }

    public void StopVoice()
    {
        voice.Stop();
    }

    public void PlayBgm(string path, float volume = defaultVolume)
    {
        AudioClip clip = Resources.Load<AudioClip>(path);
        if (clip == null)
        {
            Debug.LogWarning("Bgm Error: [" + path + "] not found");
            return;
        }
        bgm.clip = clip;
        bgm.volume = volume;
        bgm.Play();
    }

    public void PlayBgmWithStartTime(string path, float startTime, float volume = defaultVolume)
    {
        AudioClip clip = Resources.Load<AudioClip>(path);
        if (clip == null)
        {
            Debug.LogWarning("Bgm Error: [" + path + "] not found");
            return;
        }
        bgm.clip = clip;
        bgm.volume = volume;
        bgm.time = startTime;
        bgm.Play();
    }

    public void StopBgm()
    {
        bgm.Stop();
    }

    public void PlayAmbience(string path, float volume = defaultVolume)
    {
        AudioClip clip = Resources.Load<AudioClip>(path);
        if (clip == null)
        {
            Debug.LogWarning("Ambience Error: [" + path + "] not found");
            return;
        }
        ambience.clip = clip;
        ambience.volume = volume;
        ambience.Play();
    }

    public void StopAmbience()
    {
        ambience.Stop();
    }

    public void PlaySoundEffect(string path, float volume = defaultVolume)
    {
        AudioClip clip = Resources.Load<AudioClip>(path);
        if (clip == null)
        {
            Debug.LogWarning("Ambience Error: [" + path + "] not found");
            return;
        }
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
