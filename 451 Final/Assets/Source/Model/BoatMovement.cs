using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    float turnAngle = 0;
    public float velocity = 0;
    public GameObject piston;
    public GameObject rotor;
    float force = 0;
    bool reloaded = true; 
    bool firing = false;
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
            velocity += Time.deltaTime* .1f * Input.GetAxis("Vertical");
        else if (velocity > .3f)
            velocity = .3f;
        else 
            velocity = -.15f;
        Vector3 rot = transform.eulerAngles;
        transform.rotation = Quaternion.Euler(rot.x,rot.y+turnAngle*Time.deltaTime,rot.z);
        transform.GetChild(0).localRotation = Quaternion.Euler(0,-turnAngle,0);
        transform.GetChild(1).localRotation = Quaternion.Euler(0,turnAngle,0);
        transform.position += transform.forward * velocity;
        Vector3 rotorrot = rotor.transform.eulerAngles;
        rotor.transform.localRotation = Quaternion.Euler(0,0,rotorrot.z+velocity*-10);
        if(Input.GetKeyDown(KeyCode.Space)){
            if (reloaded && !firing){
                firing = true;
                reloaded = false;
            }
        }
        if (firing && force < 1){
            force += 10 * Time.deltaTime;
        } else if(!firing && !reloaded && force > 0){
            force -= .5f*Time.deltaTime;
        } else if (!firing && !reloaded){
            reloaded = true;
        } else {
            firing = false;
        }
        piston.transform.localPosition = new Vector3(0,-force,0);
    }
}
