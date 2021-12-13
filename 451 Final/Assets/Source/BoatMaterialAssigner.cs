using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BoatMaterialAssigner : MonoBehaviour
{
    public Material shader451Default;
    public Material shader451Boat;
    public Material shader451Leafs;
    public Material shaderFlag;
    public GameObject[] primitives;

    void Start()
    {
        primitives[0].GetComponent<Renderer>().material = shader451Boat;
        primitives[1].GetComponent<Renderer>().material = shader451Default;
        primitives[2].GetComponent<Renderer>().material = shader451Leafs;
        primitives[3].GetComponent<Renderer>().material = shader451Default;
        primitives[4].GetComponent<Renderer>().material = shader451Default;
        primitives[5].GetComponent<Renderer>().material = shader451Leafs;
        primitives[6].GetComponent<Renderer>().material = shaderFlag;
        primitives[7].GetComponent<Renderer>().material = shader451Default;
    }
}
