using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public bool InputMapToCircular = true;
    private float filteredForwardInput = 0f;
    private float filteredTurnInput = 0f;
    public float forwardInputFilter = 5f;
    public float turnInputFilter = 5f;
    public float forwardSpeedLimit = 1f;

    public float Forward
    {
        get;
        private set;
    }

    public float Turn
    {
        get;
        private set;
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (InputMapToCircular)
        {
            // make coordinates circular
            //based on http://mathproofs.blogspot.com/2005/07/mapping-square-to-circle.html
            h = h * Mathf.Sqrt(1f - 0.5f * v * v);
            v = v * Mathf.Sqrt(1f - 0.5f * h * h);

        }

        if (Input.GetKey(KeyCode.Q))
            h = -0.5f;
        else if (Input.GetKey(KeyCode.E))
            h = 0.5f; 

        //do some filtering of our input as well as clamp to a speed limit
        filteredForwardInput = Mathf.Clamp(Mathf.Lerp(filteredForwardInput, v,
            Time.deltaTime * forwardInputFilter), -forwardSpeedLimit, forwardSpeedLimit);
        filteredTurnInput = Mathf.Lerp(filteredTurnInput, h,
            Time.deltaTime * turnInputFilter);

        Forward = filteredForwardInput;
        Turn = filteredTurnInput;
    }
}
