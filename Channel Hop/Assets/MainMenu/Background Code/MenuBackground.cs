using UnityEngine;
using UnityEngine.Video;

public class MenuBackground : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.isLooping = true;
        videoPlayer.Play();
    }
}