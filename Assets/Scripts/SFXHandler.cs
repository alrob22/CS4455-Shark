using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class is attached to an audio source object with the same transform as the player
//allowing sfx to play from the player's position
//the sfx are chosen using a state machine
//all sounds are from freesound.org, see readme for specific citations
public class SFXHandler : MonoBehaviour
{
    public bool isRowing; //when the player is rowing, the rowing sound must play
    public bool isSharkStalked; //when a shark is actively pursing the player, the shark sound must play

    // Start is called before the first frame update
    void Start()
    {
        isRowing = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
