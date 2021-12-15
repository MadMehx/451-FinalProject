using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonScript : MonoBehaviour
{
    void OnCollisionEnter(Collision col){
        if (col.gameObject.tag == "buoy"){
            if (transform.parent.parent.parent.parent.GetComponent<BoatMovement>().IsFiring())
                col.rigidbody.AddForce(-transform.up*.02f,ForceMode.Impulse);
            else
                col.rigidbody.AddForce(-transform.up*.005f,ForceMode.Impulse);
        }
    }   
}
