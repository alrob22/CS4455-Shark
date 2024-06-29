using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//handles the end of the game
public class Endgame : MonoBehaviour
{
    public Collider endtrigger;
    public TextMeshProUGUI timerText;

    bool playerIsInside;

    // Start is called before the first frame update
    void Start()
    {
        endtrigger = this.GetComponent<Collider>();
        playerIsInside = false;
    }

    void OnCollisionEnter() {
        playerIsInside = true;
    }

//turn off the game clock and say yippee you won
    void LateUpdate() {
        if (playerIsInside) {
            timerText.text = "You won! Yippee!";
        }
    }


}
