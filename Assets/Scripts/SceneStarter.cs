using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SceneStarter : MonoBehaviour
{
    [SerializeField] private GameObject gameplayElements;
    [SerializeField] private GameObject canvas;
    [SerializeField] private bool hasStarted = false;
    
    private void Start()
    {
        // Freeze everything at the start
        Time.timeScale = 0f;
        
        // Make sure the gameplay elements are inactive at start
        if (gameplayElements != null)
            gameplayElements.SetActive(false);
    }

    public void OnConfirmButtonClicked()
    {
        VideoClip clip = null;


        if (VideoManager.Instance != null)
        {
            clip = VideoManager.Instance.GetVideo();
        }
        else
        {
            Debug.LogWarning("VideoManager.Instance is null, cannot get video clip.");
        }

        if (!hasStarted && clip != null)
        {
            StartGameScene();
        }

    }


    public void StartGameScene()
    {
        hasStarted = true;

        string currentScene = SceneManager.GetActiveScene().name;
        
        if (currentScene == "FlightSimulation_LivingRoom") {
            Loader.Load("LivingRoom");
        }

        if (gameplayElements != null)
            gameplayElements.SetActive(true);

        if (canvas != null)
            canvas.SetActive(false); // Hide the canvas
            
        // Additional initialization code
        Time.timeScale = 1f; // Ensure game time is running
        Debug.Log("Game scene started!");
    }
}