using UnityEngine;
using UnityEngine.UI;

public class VideoScroller : MonoBehaviour
{
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;
    private int currentVideo;

    private void Awake()
    {
        SelectVideo(0);
    }

    private void SelectVideo(int _index){

        previousButton.interactable = _index != 0;
        nextButton.interactable = _index != transform.childCount - 1;

        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.SetActive(i == _index);
        }
    }

    public void ChangeVideo(int _change){
        currentVideo += _change;
        SelectVideo(currentVideo);
    }
}
