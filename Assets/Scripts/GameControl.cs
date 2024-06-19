using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public GameObject uiPanel; // UI gameobject
    public Button startButton;
    public Button exitButton;

    private void Start()
    {
        Time.timeScale = 0;

        // listeners to the buttons
        startButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(ExitGame);
    }

    private void StartGame()
    {
        Time.timeScale = 1;
        // deactivate the UI panel
        uiPanel.SetActive(false);
    }

    private void ExitGame()
    {
        Application.Quit();

        // If you are in the Unity editor, stop playing the scene
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

