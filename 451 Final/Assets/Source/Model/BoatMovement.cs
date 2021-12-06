using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    float turnAngle = 0;
    float velocity = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (turnAngle <= 20 && turnAngle >= -20)
            turnAngle += Input.GetAxis("Horizontal")*.1f;
        else if (turnAngle > 20 )
            turnAngle = 20;
        else
            turnAngle = -20;
        if (velocity <= .3f && velocity >= -.15f)
            velocity += Time.deltaTime* -.1f * Input.GetAxis("Vertical");
        else if (velocity > .3f)
            velocity = .3f;
        else 
            velocity = -.15f;
        Vector3 rot = transform.eulerAngles;
        transform.rotation = Quaternion.Euler(rot.x,rot.y+turnAngle*Time.deltaTime,rot.z);
        transform.GetChild(0).localRotation = Quaternion.Euler(0,-turnAngle,0);
        transform.GetChild(1).localRotation = Quaternion.Euler(0,turnAngle,0);
        transform.position += transform.forward * velocity;
    }
}
