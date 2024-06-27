using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float startTime = 300f;
    private float currentTime;
    public TextMeshProUGUI timerText;
    public Button mainMenuStartButton;
    public Button infoScreenStartButton;
    public GameObject loseScreenCanvas;
    private bool isGamePlaying = false;

    void Start()
    {
        currentTime = startTime;
        timerText.gameObject.SetActive(false);
        mainMenuStartButton.onClick.AddListener(StartGame);
        infoScreenStartButton.onClick.AddListener(StartGame);
        loseScreenCanvas.SetActive(false);
    }

    void Update()
    {
        if (isGamePlaying)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                currentTime = 0;
                OnTimerEnd();
            }
            timerText.text = FormatTime(currentTime);
        }
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void StartGame()
    {
        isGamePlaying = true;
        currentTime = startTime;
        timerText.gameObject.SetActive(true);
        mainMenuStartButton.gameObject.SetActive(false);
        infoScreenStartButton.gameObject.SetActive(false);
    }

    void OnTimerEnd()
    {
        Debug.Log("Timer has ended!");
        loseScreenCanvas.SetActive(true);
        isGamePlaying = false;
        timerText.gameObject.SetActive(false);
        mainMenuStartButton.gameObject.SetActive(true);
        infoScreenStartButton.gameObject.SetActive(true);
    }
}
