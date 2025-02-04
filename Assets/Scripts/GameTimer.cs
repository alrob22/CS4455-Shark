using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public float startTime = 300f;
    public float currentTime;
    public TextMeshProUGUI timerText;
    public Button mainMenuStartButton;
    public Button infoScreenStartButton;
    public Button loseScreenMenuButton;
    public GameObject loseScreenCanvas;
    public GameObject winScreenCanvas;
    public GameObject mainMenuCanvas;
    public TextMeshProUGUI timeUpText;
    public TextMeshProUGUI sharkHitText;
    private bool isGamePlaying = false;
    // player position when restarting
    public GameObject player;
    private Vector3 startingPosition;
    private Quaternion startingRotation;
    public ColorChange colorChangeScript;
    public LightController lightControllerScript;

    void Start()
    {
        currentTime = startTime;
        timerText.gameObject.SetActive(false);
        mainMenuStartButton.onClick.AddListener(StartGame);
        infoScreenStartButton.onClick.AddListener(StartGame);
        loseScreenCanvas.SetActive(false);
        winScreenCanvas.SetActive(false);
        timeUpText.gameObject.SetActive(false);
        sharkHitText.gameObject.SetActive(false);
        // player pos and rot
        startingPosition = player.transform.position;
        startingRotation = player.transform.rotation;
        if (colorChangeScript != null)
        {
            colorChangeScript.ResetColors(); 
        }
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
        startingPosition = player.transform.position;
        startingRotation = player.transform.rotation;
        timerText.gameObject.SetActive(true);
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
        resetSkybox();
    }

    public void resetSkybox() {
        if (isGamePlaying == false) {
            colorChangeScript.ResetColors();
        }
    }

    public void ShowWinScreen()
    {
        winScreenCanvas.SetActive(true);
        isGamePlaying = false;
        //timerText.gameObject.SetActive(false);
        mainMenuStartButton.gameObject.SetActive(true);
        infoScreenStartButton.gameObject.SetActive(true);
        resetSkybox();
    }

    public void SharkHit()
    {
        Debug.Log("A shark hit the kayak!");
        ShowLoseScreen(sharkHitText);
    }

    public void GoToMainMenu()
    {
        colorChangeScript.ResetColors();
        lightControllerScript.ResetLight();
        SceneManager.LoadScene("Final");
        //loseScreenCanvas.SetActive(false);
        //mainMenuCanvas.SetActive(true);
        //// player pos and rot reset
        //player.transform.position = startingPosition;
        //player.transform.rotation = startingRotation;
    }
}