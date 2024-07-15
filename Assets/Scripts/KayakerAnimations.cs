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
        switch (state) {
            case KayakerState.idle:
            break;
            case KayakerState.paddleForward:
                animComponent.SetBool("AutoForward", true);
            break;
            case KayakerState.paddleLeft:
            break;
            case KayakerState.paddleRight:
            break;
            default:
            break;
        }
    }

    void setState() {
        if (kayak.ForwardAxis != 0) {
            state = KayakerState.paddleForward;
        }
    }
}
