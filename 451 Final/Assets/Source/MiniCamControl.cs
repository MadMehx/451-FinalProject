using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniCamControl : MonoBehaviour
{
    public GameObject[] buoys;
    private float timer;
    Transform target = null;
    void Start(){
        buoys = GameObject.FindGameObjectsWithTag("buoy");
        timer = 0;
    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.C)){
            timer = Time.fixedTime;
            FaceNearestBuoy();
        }
        if (Time.fixedTime - timer >4 && timer != 0){
            timer = 0;
            FaceForward();
        }
        if (target != null){
            transform.forward = Vector3.Lerp((target.position-transform.position).normalized,transform.forward,.05f);
        } else {
            transform.forward = Vector3.Lerp(-transform.parent.forward, transform.forward,.05f);
        }
    }
    void FaceNearestBuoy(){
        Vector3 temp = Vector3.zero;
        for (int i = 0; i < buoys.Length; i++){
            if (i == 0 || temp.magnitude > (transform.position - buoys[i].transform.position).magnitude){
                temp = transform.position - buoys[i].transform.position;
                target = buoys[i].transform;
            }
        } 
    }
    void FaceForward(){
        target = null;
    }
}
