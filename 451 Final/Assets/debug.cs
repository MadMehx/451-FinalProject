using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debug : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("just to suffer");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("why are we still here");
    }
}
