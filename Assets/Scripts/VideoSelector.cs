using UnityEngine;
using UnityEngine.Video;

public class VideoSelector : MonoBehaviour
{
    public VideoClip[] availableVideos; // Assign videos in the Inspector

    public void SelectVideo(int index)
    {
        if (index >= 0 && index < availableVideos.Length)
        {
            VideoManager.Instance.SetVideo(availableVideos[index]);
        }
    }
}
