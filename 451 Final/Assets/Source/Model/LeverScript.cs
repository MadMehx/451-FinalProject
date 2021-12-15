using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour
{
    public GameObject motor;
    public GameObject sphere10;
    public GameObject sphere20;
    public GameObject sphere30;
    public GameObject sphere40;
    float turn = 0;
    float move = -50;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   transform.parent.parent.position = motor.transform.position;
        transform.parent.parent.rotation = motor.transform.parent.rotation;
        transform.parent.localRotation = Quaternion.Euler(0,180,0);

        if (Input.GetMouseButton(0)){
            move += Input.GetAxis("Mouse Y");
            turn += Input.GetAxis("Mouse X");
        }
        if ( move < -89.99f)
            move = -89.99f;
        else if (move > -50)
            move = -50;
        if ( turn < -30)
            turn = -30;
        else if ( turn > 30)
            turn = 30;
        Debug.Log(Input.GetAxis("Mouse Y") + "     " + Input.GetAxis("Mouse X"));
        transform.localRotation = Quaternion.Euler(move,-turn,0);
        motor.transform.localRotation = Quaternion.Euler(-(move+90),-(turn),0);
        sphere10.transform.localPosition = new Vector3(Mathf.Cos(10*Mathf.Deg2Rad)*Mathf.Sin(turn*Mathf.Deg2Rad),Mathf.Sin(10*Mathf.Deg2Rad),Mathf.Cos(10*Mathf.Deg2Rad)*-Mathf.Cos(turn*Mathf.Deg2Rad)); 
        sphere20.transform.localPosition = new Vector3(Mathf.Cos(20*Mathf.Deg2Rad)*Mathf.Sin(turn*Mathf.Deg2Rad),Mathf.Sin(20*Mathf.Deg2Rad),Mathf.Cos(20*Mathf.Deg2Rad)*-Mathf.Cos(turn*Mathf.Deg2Rad)); 
        sphere30.transform.localPosition = new Vector3(Mathf.Cos(30*Mathf.Deg2Rad)*Mathf.Sin(turn*Mathf.Deg2Rad),Mathf.Sin(30*Mathf.Deg2Rad),Mathf.Cos(30*Mathf.Deg2Rad)*-Mathf.Cos(turn*Mathf.Deg2Rad)); 
        sphere40.transform.localPosition = new Vector3(Mathf.Cos(40*Mathf.Deg2Rad)*Mathf.Sin(turn*Mathf.Deg2Rad),Mathf.Sin(40*Mathf.Deg2Rad),Mathf.Cos(40*Mathf.Deg2Rad)*-Mathf.Cos(turn*Mathf.Deg2Rad)); 

    }
}
