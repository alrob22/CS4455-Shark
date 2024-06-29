using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class handles two secret continuous audio loops in the background
//that sound like constant paddling and constantly being chased by the shark
//these audio loops are muted and unmuted using this script, according to
//when the player performs certain actions
public class SFXHandler : MonoBehaviour
{   
    public GameObject Player; //see if the player has moved. if they have, keep playing the rowing sound. if no movement, mute the rowing sound
    Vector3 playerPrevPos;
    public AudioSource rowing;
    public AudioSource shark;
    public bool isRowing; //when the player is rowing, the rowing sound must play
    public bool isSharkStalked; //when a shark is actively pursing the player, the shark sound must play

    // Start is called before the first frame update
    void Start()
    {
        isRowing = false;
        isSharkStalked = false;
        playerPrevPos = Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerPrevPos != Player.transform.position) {
            isRowing = true;
        } else {
            isRowing = false;
        }

        if(isRowing) {
            rowing.mute = false; //add fade out here
        } else {
            rowing.mute = true; //add fade in here
        }

        if(isSharkStalked) {
            shark.mute = false; //add fade out here
        } else {
            shark.mute = true; //add fade in here
        }
        
        playerPrevPos = Player.transform.position;
    }
}
