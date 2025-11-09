using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public static VideoManager Instance;

    public VideoPlayer videoPlayer;
    public VideoClip startClip;
    public VideoClip endClip;

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    [ContextMenu("Play Start")]
    public void PlayStartClip() 
    {
        videoPlayer.clip = startClip;
        videoPlayer.Play();
    }

    [ContextMenu("Play End")]
    public void PlayEndClip() 
    {
        videoPlayer.clip = endClip;
        videoPlayer.Play();
    }

}
