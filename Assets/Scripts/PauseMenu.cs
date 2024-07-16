using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject infoCanvas;
    public GameObject player;
    public GameObject timer;
    public GameObject startButton;
    private Vector3 startingPosition;
    private Quaternion startingRotation;
    private float startTime;
    private bool isGamePaused = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseGame();
        }
    }

    void TogglePauseGame()
    {
        if (isGamePaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f; // Pause the game
        infoCanvas.SetActive(true);
        startButton.SetActive(false);
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f; // Resume the game
        infoCanvas.SetActive(false);
        startButton.SetActive(true);
    }
}
