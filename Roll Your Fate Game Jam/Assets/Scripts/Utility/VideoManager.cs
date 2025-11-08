using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip startClip;
    public VideoClip endClip;

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
