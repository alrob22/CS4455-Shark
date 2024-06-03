using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private Transform enemy;
    public GameObject player;
    public float speed = 1f;

    void Start()
    {
        enemy = GetComponent<Transform>();
    }

    void Update()
    {
        enemy.LookAt(player.transform);
        enemy.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
