using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameControl : MonoBehaviour
{
    public GameObject uiPanel; // UI gameobject
    public Button startButton;
    public Button exitButton;
    public GameObject textContainer; // controls

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

    private void ExitGame()
    {
        Application.Quit();

        // If you are in the Unity editor, stop playing the scene
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

