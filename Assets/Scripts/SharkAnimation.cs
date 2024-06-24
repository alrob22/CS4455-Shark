using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkAnimation : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void PauseAnimation()
    {
        anim.speed = 0;
    }
}
