using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BoatMaterialAssigner : MonoBehaviour
{
    public Material shader451;
    public GameObject[] primitives;

    void Start()
    {
        foreach (GameObject prim in primitives)
        {
            prim.GetComponent<Renderer>().material = shader451;
        }
    }
}
