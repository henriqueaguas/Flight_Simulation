using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public static VideoManager Instance;
    public VideoClip selectedVideo;

    void Awake()
    {
        // Make sure only one instance of VideoManager exists across scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetVideo(VideoClip video)
    {
        selectedVideo = video;
    }

    public VideoClip GetVideo()
    {
        return selectedVideo;
    }
}