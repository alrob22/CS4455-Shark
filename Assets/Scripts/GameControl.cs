using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameControl : MonoBehaviour
{
    public GameObject uiPanel; // UI gameobject
    public GameObject textContainer; // controls
    public GameObject howToPlayCanvas; // How to Play canvas
    public Button startButton;
    public Button exitButton;
    public Button infoButton; // "How to Play" button
    public Button backButton; // back button to return from "How to Play"
    public Button infoStartButton;
    public GameObject infoStartButtonGO;

    private void Start()
    {
        Time.timeScale = 0;
        howToPlayCanvas.SetActive(false);
        // listeners to the buttons
        startButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(ExitGame);
        infoButton.onClick.AddListener(ShowHowToPlay);
        backButton.onClick.AddListener(HideHowToPlay);
        infoStartButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        Time.timeScale = 1;
        // deactivate the UI panel
        uiPanel.SetActive(false);
        howToPlayCanvas.SetActive(false);
        StartCoroutine(HideTextBoxesAfterDelay(20f));
    }

    private void Update()
    {
        // 'C' key pressed?
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(ShowControlsForDuration(10f));
        }
    }

    private IEnumerator HideTextBoxesAfterDelay(float delay)
    {
        // delay
        yield return new WaitForSeconds(delay);
        // deactivate the text container
        textContainer.SetActive(false);
    }

    private IEnumerator ShowControlsForDuration(float duration)
    {
        // activate the text container
        textContainer.SetActive(true);
        // delay
        yield return new WaitForSeconds(duration);
        // deactivate the text container
        textContainer.SetActive(false);
    }

    private void ShowHowToPlay()
    {
        uiPanel.SetActive(false);
        howToPlayCanvas.SetActive(true);
        infoStartButtonGO.SetActive(true);
    }

    private void HideHowToPlay()
    {
        howToPlayCanvas.SetActive(false);
        uiPanel.SetActive(true);
        infoStartButtonGO.SetActive(false);
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

