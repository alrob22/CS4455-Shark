using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class BasicControl : MonoBehaviour
{
    private BasicMovement cCtrl;
    private Rigidbody rb;

    public float forwardMaxSpeed = 1f;
    public float turnMaxSpeed = 1f;

    void Awake()
    {
        cCtrl = GetComponent<BasicMovement>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float inputForward = 0f;
        float inputTurn = 0f;

        if (cCtrl.enabled)
        {
            inputForward = cCtrl.Forward;
            inputTurn = cCtrl.Turn;
        }

        if (inputForward < 0f)
        {
            inputTurn = -inputTurn;
        }

        rb.MovePosition(rb.position + this.transform.forward * inputForward * Time.deltaTime * forwardMaxSpeed);
        rb.MoveRotation(rb.rotation * Quaternion.AngleAxis(inputTurn * Time.deltaTime * turnMaxSpeed, Vector3.up));
    }
}
