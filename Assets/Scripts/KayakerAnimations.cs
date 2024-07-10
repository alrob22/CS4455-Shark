using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KayakerAnimations : MonoBehaviour
{
    Animator animComponent;
    void Start()
    {
     animComponent = GetComponent<Animator>(); //fetch the animator this script is attached to
    }

    void Update()
    {
        //Trigger animation when KeyCode.Space is pressed
        if (Input.GetKey(KeyCode.W))
        {
            animComponent.SetBool("AutoForward", true);
        } else {
            animComponent.SetBool("AutoForward", false);
        }
    }
}
