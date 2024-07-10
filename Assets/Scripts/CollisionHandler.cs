using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CollisionHandler : MonoBehaviour
{
    public float boost = 25f;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            KayakMovement kayakMovement = collision.GetComponent<KayakMovement>();
            if (kayakMovement != null) {
                kayakMovement.ResetForwardForce(boost);
                Debug.Log("Increased forward force by: " + boost);
            }
            Destroy(gameObject); 
        }
    }
}
