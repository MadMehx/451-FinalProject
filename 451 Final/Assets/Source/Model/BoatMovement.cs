using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    public GameObject boat;
    public float turnRate = 0.1f;
    public float accelRate;

    float turnAngle = 0;
    float velocity = 0;
    public GameObject piston;
    public GameObject motor;
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
        if (boat == GameManagerASL.playerBoat)
        {
            RotationNetworked();
            MovementNetworked();
            Piston();
        }
    }

    private void MovementNoNetwork()
    {
        if (turnAngle <= 20 && turnAngle >= -20)
            turnAngle += Input.GetAxis("Horizontal") * .1f;
        else if (turnAngle > 20)
            turnAngle = 20;
        else
            turnAngle = -20;

        if (velocity <= .1f && velocity >= -.15f)
            velocity += Time.deltaTime * .1f * Input.GetAxis("Vertical");
        else if (velocity > .1f)
            velocity = .1f;
        else
            velocity = -.15f;

        Vector3 rot = transform.eulerAngles;
        transform.rotation = Quaternion.Euler(rot.x, rot.y + turnAngle * Time.deltaTime, rot.z);

        transform.GetChild(0).localRotation = Quaternion.Euler(0, -turnAngle, 0);
        transform.GetChild(1).localRotation = Quaternion.Euler(0, turnAngle, 0);

        transform.position -= transform.forward * velocity;

        Vector3 rotorrot = rotor.transform.eulerAngles;
        rotor.transform.localRotation = Quaternion.Euler(0, 0, rotorrot.z + velocity * 10);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (reloaded && !firing)
            {
                firing = true;
                reloaded = false;
            }
        }
        if (firing && force < 4)
        {
            force += 10 * Time.deltaTime;
        }
        else if (!firing && !reloaded && force > 0)
        {
            force -= .5f * Time.deltaTime;
        }
        else if (!firing && !reloaded)
        {
            reloaded = true;
        }
        else
        {
            firing = false;
        }
        piston.transform.localPosition = new Vector3(0, -force, 0);
    }

    private void RotationNetworked()
    {
        // Get input
        // turnAngle = Mathf.Clamp(
        //     turnAngle + Input.GetAxis("Horizontal") * turnRate,
        //     -30,
        //     30);
        turnAngle = -motor.transform.localEulerAngles.y;
        if (turnAngle < -90)
            turnAngle+=360;

        // Calculate new rotation
        var currRot = transform.eulerAngles;
        var newRot = Quaternion.Euler(currRot.x,
                                      currRot.y + turnAngle * Time.deltaTime * 10, //turning way too slowly
                                      currRot.z);
        // Send new rotation
        float[] direction = new float[]
        {
            newRot.eulerAngles.x,
            newRot.eulerAngles.y,
            newRot.eulerAngles.z,
            ASL.GameLiftManager.GetInstance().m_PeerId + 10f
        };
        GameManagerASL.playerBoat.GetComponent<ASL.ASLObject>().SendFloatArray(direction);

        // Locally rotate engine, piston, and rotor
        //transform.GetChild(0).localRotation = Quaternion.Euler(0, -turnAngle, 0);
        transform.GetChild(1).localRotation = Quaternion.Euler(0, turnAngle, 0);
        var rotorRot = rotor.transform.eulerAngles;
        rotor.transform.localRotation = Quaternion.Euler(0, 0, rotorRot.z + velocity);
    }

    private void MovementNetworked()
    {
        // Get Input
        // velocity = Mathf.Clamp(
        //     velocity + Input.GetAxis("Vertical") * accelRate * Time.deltaTime,
        //     0,
        //     0.4f);
        velocity = (motor.transform.eulerAngles.x -320) * .1f;

        // Calculate new movement
        var moveAmount = -transform.forward * velocity;
        // Send new movement
        float[] direction = new float[]
        {
            moveAmount.x,
            0.0f,
            moveAmount.z,
            0.0f
        };
        GameManagerASL.playerBoat.GetComponent<ASL.ASLObject>().SendFloatArray(direction);
    }

    private void Piston()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (reloaded && !firing)
            {
                firing = true;
                reloaded = false;
            }
        }

        if (firing && force < 5)
        {
            force += 10 * Time.deltaTime;
        }
        else if (!firing && !reloaded && force > 0)
        {
            force -= .5f * Time.deltaTime;
        }
        else if (!firing && !reloaded)
        {
            reloaded = true;
        }
        else
        {
            firing = false;
        }
        piston.transform.localPosition = new Vector3(0, -force, 0);
    }
    public bool IsFiring(){
        return firing;
    }
}
