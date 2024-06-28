using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KayakCollisionHandler : MonoBehaviour
{
    public GameTimer gameTimer;

    void Start()
    {
        if (gameTimer == null)
        {
            gameTimer = FindObjectOfType<GameTimer>();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameTimer.SharkHit();
        }
    }
}