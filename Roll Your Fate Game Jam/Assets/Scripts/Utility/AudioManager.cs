using UnityEngine;

[System.Serializable]
public struct SFXList
{
    public AudioClip OnClick;
    public AudioClip OnFailed;
    public AudioClip OnInteract2;
    public AudioClip OnInteract;
    public AudioClip OnSelect;
    public AudioClip OnSuccess;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public SFXList sfx;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip);
    }

    public void Click() { PlaySFX(sfx.OnClick); }
    public void Failed() { PlaySFX(sfx.OnFailed); }
    public void Interact2() { PlaySFX(sfx.OnInteract2); }
    public void Interact() { PlaySFX(sfx.OnInteract); }
    public void Select() { PlaySFX(sfx.OnSelect); }
    public void Success() { PlaySFX(sfx.OnSuccess); }
}
