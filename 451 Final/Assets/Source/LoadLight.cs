using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLight : MonoBehaviour {
    public Transform LightPosition;
    public Transform LightPosition1;

    void Update()
    {
        Shader.SetGlobalVector("LightPosition", LightPosition.position);
        //Shader.SetGlobalVector("LightPosition1", LightPosition1.position);
    }
}
