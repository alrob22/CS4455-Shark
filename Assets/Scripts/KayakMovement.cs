using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class KayakMovement : MonoBehaviour
{
    #region UI 
    public TextMeshProUGUI textA;
    public TextMeshProUGUI textD;
    public TextMeshProUGUI textQ;
    public TextMeshProUGUI textE;
    public TextMeshProUGUI textLeft;
    public TextMeshProUGUI textRight;
    public TextMeshProUGUI textS;
    public TextMeshProUGUI textW;
    public Slider forwardSlider;
    public Image sliderFill;
    //public TextMeshProUGUI textCTRL;
    //public TextMeshProUGUI textSpace;

    #endregion

    [Header("Normal Paddle")]
    public float normalPaddleTurnForce = 10f;
    public float normalPaddleForwardForce = 10f;
    public float normalRudderForce = 2f;
    public float normalSmoothForceSpeed = 1f;

    [Header("Back Paddle")]
    public float backPaddleForce = 10f;

    [Header("Wide Paddle")]
    public float widePaddleTurnForce = 20f;
    public float widePaddleForwardForce = 5f;
    public float wideSmoothForceSpeed = 1f;

    [Header("Side Paddle")]
    public float sidePaddleForce = 5f;
    public float sideSmoothForceSpeed = 1f;

    [Header("Edge")]
    public float edgeTurnInfluence = 5f;
    public float edgeSideInfluence = 5f;

    [Header("Forward")]
    public float forwardForce = 10f;
    public float forwardTimer = 10f;
    public float forwardTimerSpeed = 1f;
    private float forwardTimerCap;
    private bool goForward = true;

    [Header("Stroke State")]
    public Stroke currentStroke = Stroke.None;
    public Stroke lastStroke = Stroke.None;
    public StrokeState currentStrokeState = StrokeState.Ready;
    public Stroke CurrentStroke { get { return currentStroke; } }
    public StrokeState CurrentStrokeState { get { return currentStrokeState; } }

    [Header("Water")]
    public bool isMovingWater = false;
    public float movingWaterForce = 2f;
    public float waterDirDownForce = 2f;
    public float waterDownForceAngle = 10f;
    public LayerMask whatIsWater;
    private Vector3 currentWaterUpDir;
    private Vector3 lastWaterUpDir;

    private float normalStrokeAxis;
    private float wideStrokeAxis;
    private float sideStrokeAxis;
    private float backStrokeAxis;
    private float rudderAxis;
    private float edgeAxis;
    private float forwardAxis;

    public float NormalStrokeAxis { get { return normalStrokeAxis; } }
    public float WideStrokeAxis { get { return wideStrokeAxis; } }
    public float SideStrokeAxis { get { return sideStrokeAxis; } }
    public float BackStrokeAxis { get { return backStrokeAxis; } }
    public float RudderAxis { get { return rudderAxis; } }
    public float EdgeAxis { get { return edgeAxis; } }
    public float ForwardAxis {  get { return forwardAxis; } }

    private float currentPaddleNormalForwardForce = 0f;
    private float currentPaddleWideForwardForce = 0f;
    private float currentPaddleSideForwardForce = 0f;
    private float currentPaddleNormalRotateForce = 0f;
    private float currentPaddleWideRotateForce = 0f;

    private float currentNrmlVel = 0;
    private float currentWideVel = 0;
    private float currentSideVel = 0;
    private float currentNrmlRotVel = 0;
    private float currentWideRotVel = 0;

    private Quaternion gravityAlignment = Quaternion.identity;

    [Header("Camera")]
    public Camera mainCam;
    public Camera behindCam;
    private float lookBehind;

    private Rigidbody rb;
    private PlayerInput playerInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        currentStrokeState = StrokeState.Ready;

        forwardTimerCap = forwardTimer;
        forwardSlider.maxValue = forwardTimerCap;

        mainCam.enabled = true;
        behindCam.enabled = false;
    }

    void Update()
    {
        HandlePlayerInput();
        SetStrokeState();
        AlignRotation();
        UpdateText();
    }

    void FixedUpdate()
    {
        HandleBoatMovement();
    }

    void HandlePlayerInput()
    {
        normalStrokeAxis = playerInput.actions["NormalStroke"].ReadValue<float>();
        wideStrokeAxis = playerInput.actions["WideStroke"].ReadValue<float>();
        sideStrokeAxis = playerInput.actions["SideStroke"].ReadValue<float>();
        backStrokeAxis = playerInput.actions["BackStroke"].ReadValue<float>();
        rudderAxis = playerInput.actions["RudderAxis"].ReadValue<float>();
        edgeAxis = playerInput.actions["EdgeAxis"].ReadValue<float>();
        forwardAxis = playerInput.actions["Forward"].ReadValue<float>();
        lookBehind = playerInput.actions["LookBehind"].ReadValue<float>();

        // remove w thing stopping
    }

    void SetStrokeState()
    {
        if (normalStrokeAxis != 0)
        {
            currentStroke = lastStroke = Stroke.Normal;
        }
        if (wideStrokeAxis != 0)
        {
            currentStroke = lastStroke = Stroke.Wide;
        }
        if (sideStrokeAxis != 0)
        {
            currentStroke = lastStroke = Stroke.Side;
        }
        if (backStrokeAxis != 0)
        {
            currentStroke = lastStroke = Stroke.Back;
        }

        if (normalStrokeAxis == 0)
        {
            ResetNormalStrokeForces();
        }
        if (wideStrokeAxis == 0)
        {
            ResetWideStrokeForces();
        }
        if (sideStrokeAxis == 0)
        {
            ResetSideStrokeForces();
        }

        // If no input set stroke to none
        if (normalStrokeAxis == 0 && wideStrokeAxis == 0 && sideStrokeAxis == 0 && backStrokeAxis == 0)
        {
            currentStroke = Stroke.None;
            currentStrokeState = StrokeState.Ready;
        }

        if (normalStrokeAxis != 0 && (wideStrokeAxis != 0 || sideStrokeAxis != 0 || backStrokeAxis != 0))
        {
            currentStroke = Stroke.Normal;
        }
        else if (wideStrokeAxis != 0 && (normalStrokeAxis != 0 || sideStrokeAxis != 0 || backStrokeAxis != 0))
        {
            currentStroke = Stroke.Wide;
        }
        else if (sideStrokeAxis != 0 && (normalStrokeAxis != 0 || wideStrokeAxis != 0 || backStrokeAxis != 0))
        {
            currentStroke = Stroke.Side;
        }
        else if (backStrokeAxis != 0 && (normalStrokeAxis != 0 || wideStrokeAxis != 0 || sideStrokeAxis != 0))
        {
            currentStroke = Stroke.Back;
        }

        // If ready for the next stroke and player is performing a stroke, set state to midstroke
        if (currentStrokeState == StrokeState.Ready && currentStroke != Stroke.None)
        {
            currentStrokeState = StrokeState.MidStroke;
        }
    }

    void ResetNormalStrokeForces()
    {
        currentPaddleNormalForwardForce = Mathf.SmoothDamp(currentPaddleNormalForwardForce, 0, ref currentNrmlVel, normalSmoothForceSpeed);
        if (currentPaddleNormalForwardForce != 0 && currentPaddleNormalForwardForce < 0.1)
        {
            currentPaddleNormalForwardForce = 0;
        }

        currentPaddleNormalRotateForce = Mathf.SmoothDamp(currentPaddleNormalRotateForce, 0, ref currentNrmlRotVel, normalSmoothForceSpeed);
        if (currentPaddleNormalRotateForce != 0 && currentPaddleNormalRotateForce < 0.1)
        {
            currentPaddleNormalRotateForce = 0;
        }
    }

    void ResetWideStrokeForces()
    {
        currentPaddleWideForwardForce = Mathf.SmoothDamp(currentPaddleWideForwardForce, 0, ref currentWideVel, wideSmoothForceSpeed);
        if (currentPaddleWideForwardForce != 0 && currentPaddleWideForwardForce < 0.1)
        {
            currentPaddleWideForwardForce = 0;
        }

        currentPaddleWideRotateForce = Mathf.SmoothDamp(currentPaddleWideRotateForce, 0, ref currentWideRotVel, wideSmoothForceSpeed);
        if (currentPaddleWideRotateForce != 0 && currentPaddleWideRotateForce < 0.1)
        {
            currentPaddleWideRotateForce = 0;
        }
    }

    void ResetSideStrokeForces()
    {
        currentPaddleSideForwardForce = Mathf.SmoothDamp(currentPaddleSideForwardForce, 0, ref currentSideVel, sideSmoothForceSpeed);
        if (currentPaddleSideForwardForce != 0 && currentPaddleSideForwardForce < 0.1)
        {
            currentPaddleSideForwardForce = 0;
        }
    }

    void HandleBoatMovement()
    {
        // Apply downforce towards water normal
        rb.AddForce(-currentWaterUpDir * waterDirDownForce);

        // If the water is moving we can control the speed here
        if (isMovingWater)
        {
            rb.AddForce(Vector3.forward * movingWaterForce * Time.deltaTime);
        }

        // Depending on the stroke, add the relevant motions
        if (currentStrokeState == StrokeState.MidStroke)
        {
            switch (currentStroke)
            {
                case Stroke.None:
                    break;

                case Stroke.Normal:
                    currentPaddleNormalForwardForce = Mathf.SmoothDamp(currentPaddleNormalForwardForce, normalPaddleForwardForce, ref currentNrmlVel, normalSmoothForceSpeed);
                    rb.AddForce(transform.forward * currentPaddleNormalForwardForce * Time.deltaTime);

                    currentPaddleNormalRotateForce = Mathf.SmoothDamp(currentPaddleNormalRotateForce, normalPaddleTurnForce, ref currentNrmlRotVel, normalSmoothForceSpeed);
                    rb.AddRelativeTorque(Vector3.up * -normalStrokeAxis * currentPaddleNormalRotateForce * Time.deltaTime);

                    break;

                case Stroke.Wide:
                    currentPaddleWideForwardForce = Mathf.SmoothDamp(currentPaddleWideForwardForce, widePaddleForwardForce, ref currentWideVel, wideSmoothForceSpeed);
                    rb.AddForce(transform.forward * currentPaddleWideForwardForce * Time.deltaTime);

                    currentPaddleWideRotateForce = Mathf.SmoothDamp(currentPaddleWideRotateForce, widePaddleTurnForce, ref currentNrmlRotVel, wideSmoothForceSpeed);
                    rb.AddRelativeTorque(Vector3.up * -wideStrokeAxis * currentPaddleWideRotateForce * Time.deltaTime);

                    break;

                case Stroke.Side:
                    currentPaddleSideForwardForce = Mathf.SmoothDamp(currentPaddleSideForwardForce, sidePaddleForce, ref currentSideVel, sideSmoothForceSpeed);
                    rb.AddForce(transform.right * sideStrokeAxis * currentPaddleSideForwardForce * Time.deltaTime);

                    break;

                case Stroke.Back:
                    rb.AddForce(-transform.forward * backStrokeAxis * backPaddleForce * Time.deltaTime);

                    break;
            }
        }

        // Auto forward movement
        if (forwardAxis > 0 && goForward)
        {
            if (forwardTimer > 0)
            {
                forwardTimer -= forwardTimerSpeed * Time.deltaTime;

                rb.AddForce(transform.forward * forwardAxis * forwardForce * (Time.deltaTime * 1.5f));
            }
        } else if (forwardAxis == 0 && forwardTimer < forwardTimerCap && goForward)
        {
            goForward = false;
        } else if (forwardAxis == 0 && !goForward)
        {
            if (forwardTimer < forwardTimerCap)
            {
                forwardTimer += (forwardTimerSpeed / 3) * Time.deltaTime;
            } else
            {
                goForward = true;
            }
        }

        Debug.Log("forwardTimer = " + forwardTimer);
        Debug.Log("goForward = " + goForward);

        // Handle boat ruddering
        if (currentStrokeState == StrokeState.Ruddering)
        {
            rb.AddRelativeTorque(Vector3.up * -rudderAxis * normalRudderForce * Time.deltaTime);
        }

        // Handle boat edging
        if (rb.velocity.magnitude > .15f && (edgeAxis > 0.1 || edgeAxis < -0.1))
        {
            rb.AddRelativeTorque(Vector3.up * edgeAxis * edgeTurnInfluence * Time.deltaTime);
            rb.AddForce(transform.right * edgeAxis * edgeSideInfluence * Time.deltaTime);
        }

        // Handle camera movement
        if (lookBehind > 0)
        {
            mainCam.enabled = false;
            behindCam.enabled = true;
        } else
        {
            mainCam.enabled = true;
            behindCam.enabled = false;
        }
    }

    void AlignRotation()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 5f, whatIsWater, QueryTriggerInteraction.Ignore))
        {
            currentWaterUpDir = lastWaterUpDir = hitInfo.normal;
        }
    }

    void UpdateText()
    {
        // normal stroke- A/D
        if (normalStrokeAxis < 0)
        {
            textA.color = Color.yellow;
        } else
        {
            textA.color = Color.black;
        }
        if (normalStrokeAxis > 0)
        {
            textD.color = Color.yellow;
        } else
        {
            textD.color = Color.black;
        }

        // wide stroke- Q/E
        if (wideStrokeAxis < 0)
        {
            textQ.color = Color.yellow;
        } else
        {
            textQ.color = Color.black;
        }
        if (wideStrokeAxis > 0)
        {
            textE.color = Color.yellow;
        } else
        {
            textE.color = Color.black;
        }

        // side stroke- left/right arrows
        if (sideStrokeAxis < 0)
        {
            textLeft.color = Color.yellow;
        } else
        {
            textLeft.color = Color.black;
        }
        if (sideStrokeAxis > 0)
        {
            textRight.color = Color.yellow;
        } else
        {
            textRight.color = Color.black;
        }

        // back stroke- S
        if (backStrokeAxis != 0)
        {
            textS.color = Color.yellow;
        } else
        {
            textS.color = Color.black;
        }

        // auto forward- W
        if (forwardAxis != 0)
        {
            textW.color = Color.yellow;
        }
        else
        {
            textW.color = Color.black;
        }

        forwardSlider.value = forwardTimer;
        if (forwardSlider.value == 0)
        {
            sliderFill.color = Color.black;
        } else if (forwardSlider.value == forwardTimerCap)
        {
            sliderFill.color = Color.yellow;
        }

        // edge/lean- CTRL/Space
        //if (edgeAxis < 0)
        //{
        //    textCTRL.color = Color.yellow;
        //} else
        //{
        //    textCTRL.color = Color.black;
        //}
        //if (edgeAxis > 0)
        //{
        //    textSpace.color = Color.yellow;
        //} else
        //{
        //    textSpace.color = Color.black;
        //}
    }
}
