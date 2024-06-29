using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//handles the end of the game
public class Endgame : MonoBehaviour
{
    public Collider endtrigger;
    public TextMeshProUGUI timerText;

    public bool playerIsInside;

    public GameTimer gameclock;

    // Start is called before the first frame update
    void Start()
    {
        playerIsInside = false;
    }

    void OnTriggerEnter(Collider obj) {
        if (obj.gameObject.tag == "Player") {
            gameclock.isGamePlaying = false;
            playerIsInside = true;
        }
    }

//turn off the game clock and say yippee you won
    void Update() {
        if (playerIsInside) {
            timerText.text = "You won! Yippee!";
        }
    }


}
