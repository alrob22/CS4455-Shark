using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KayakerAnimations : MonoBehaviour
{
    public KayakMovement kayak;
    public enum KayakerState {
        paddleForward, paddleLeft, paddleRight, paddleWideLeft, paddleWideRight, paddleBack, idle
    }
    Animator animComponent;
    public KayakerState state;
    void Start()
    {
        state = KayakerState.idle;
        animComponent = GetComponent<Animator>(); //fetch the animator this script is attached to
    }

    void Update()
    {
        setState();
        animComponent.SetBool("autoforward", false);
        animComponent.SetBool("back", false);
        //reset speed to default .25
        switch (state) {
            case KayakerState.idle:
                //add idle after button is released
                animComponent.SetBool("idle", true);
                animComponent.SetBool("paddle", false);
            break;
            case KayakerState.paddleForward:
                animComponent.SetBool("idle", false);
                animComponent.SetBool("autoforward", kayak.goForward);
                //animComponent.SetFloat("LeftOrRight", 0.5f);
            break;
            case KayakerState.paddleLeft:
                //set blend to 0
                animComponent.SetBool("idle", false);
                animComponent.SetBool("paddle", true);
                animComponent.SetFloat("LeftOrRight", 0.0f);
            break;
            case KayakerState.paddleRight:
                //set blend to 1
                //animComponent.SetBool("Right", true);
                animComponent.SetBool("idle", false);
                animComponent.SetBool("paddle", true);
                animComponent.SetFloat("LeftOrRight", 1.0f);
            break;
            case KayakerState.paddleWideLeft:
                animComponent.SetBool("idle", false);
                animComponent.SetBool("paddle", true);
                animComponent.SetFloat("LeftOrRight", 3.0f);
                //animComponent.SetTrigger("wide");
            break;
            case KayakerState.paddleWideRight:
                animComponent.SetBool("idle", false);
                animComponent.SetBool("paddle", true);
                animComponent.SetFloat("LeftOrRight", 2.0f);
                //animComponent.SetTrigger("wide");
            break;
            case KayakerState.paddleBack:
                animComponent.SetBool("idle", false);
                animComponent.SetBool("back", true);
            break;
            default:
            break;
        }
    }

    void setState() {
        /*if (kayak.ForwardAxis != 0) {
            state = KayakerState.paddleForward;
        } else {
            state = KayakerState.idle;
        } */
        if(kayak.currentStroke == Stroke.None) {
            if(Input.GetKey(KeyCode.W)) {
                state = KayakerState.paddleForward;
            } else {
                state = KayakerState.idle;
            }
        } else if(kayak.currentStroke == Stroke.Normal) {
            //Debug.Log("stroking");
            if (Input.GetKey(KeyCode.A)) {
                state = KayakerState.paddleLeft; 
            } else if (Input.GetKey(KeyCode.D)) {
                state = KayakerState.paddleRight;
            }
        } else if(kayak.currentStroke == Stroke.Wide) 
        {
            if (Input.GetKey(KeyCode.Q)) {
                state = KayakerState.paddleWideLeft;
            } else if (Input.GetKey(KeyCode.E)) {
                state = KayakerState.paddleWideRight;
            }
    } else if(kayak.currentStroke == Stroke.Back) 
        {
            state = KayakerState.paddleBack;
        }
}
}
