using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    private bool isInitialized = false;

    void OnEnable()
    {
        if (!isInitialized)
        {
            videoPlayer = GetComponent<VideoPlayer>();

            VideoClip clip = VideoManager.Instance.GetVideo();
            if (clip != null)
            {
                videoPlayer.clip = clip;
                videoPlayer.Play();
                isInitialized = true;
            }
            else
            {
                Debug.LogWarning("No video clip selected.");
            }
        }
    }
}
