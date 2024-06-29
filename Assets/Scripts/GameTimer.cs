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
    public Button loseScreenMenuButton;
    public GameObject loseScreenCanvas;
    public GameObject mainMenuCanvas;
    public TextMeshProUGUI timeUpText;
    public TextMeshProUGUI sharkHitText;
    public bool isGamePlaying = false;

    void Start()
    {
        currentTime = startTime;
        timerText.gameObject.SetActive(false);
        mainMenuStartButton.onClick.AddListener(StartGame);
        infoScreenStartButton.onClick.AddListener(StartGame);
        loseScreenCanvas.SetActive(false);
        timeUpText.gameObject.SetActive(false);
        sharkHitText.gameObject.SetActive(false);
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
        loseScreenCanvas.SetActive(false);
        timeUpText.gameObject.SetActive(false);
        sharkHitText.gameObject.SetActive(false);
    }

    void OnTimerEnd()
    {
        Debug.Log("Timer has ended!");
        ShowLoseScreen(timeUpText);
    }

    void ShowLoseScreen(TextMeshProUGUI loseMessage)
    {
        loseMessage.gameObject.SetActive(true);
        loseScreenCanvas.SetActive(true);
        isGamePlaying = false;
        timerText.gameObject.SetActive(false);
        mainMenuStartButton.gameObject.SetActive(true);
        infoScreenStartButton.gameObject.SetActive(true);
    }

    public void SharkHit()
    {
        Debug.Log("A shark hit the kayak!");
        ShowLoseScreen(sharkHitText);
    }

    public void GoToMainMenu()
    {
        loseScreenCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }
}