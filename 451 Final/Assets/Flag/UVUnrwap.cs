using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Matrix3x3Helpers;
//using static Matrix3x3;

public class UVUnrwap : MonoBehaviour
{
    public Vector2 Position = new Vector2(0, 0);
    public Vector2 Scale = new Vector2(1, 1);
    public float Rotation = 0f;
    public Vector2 RotationPoint = new Vector2(0, 0);
    Vector2[] initUV = null;

    public bool anim = false;

    //Matrix3x3 ones = new Matrix3x3();
    //Matrix3x3 os = new Matrix3x3();

    public void InitUV(Vector2[] uv)
    {
        initUV = new Vector2[uv.Length];
        for (int i = 0; i < uv.Length; i++)
        {
            initUV[i] = uv[i];
        }
        /**
        ones.m00 = 1;
        ones.m01 = 1;
        ones.m02 = 1;
        ones.m10 = 1;
        ones.m11 = 1;
        ones.m12 = 1;
        ones.m20 = 1;
        ones.m21 = 1;
        ones.m22 = 1;

        os.m00 = 0;
        os.m01 = 0;
        os.m02 = 0;
        os.m10 = 0;
        os.m11 = 0;
        os.m12 = 0;
        os.m20 = 0;
        os.m21 = 0;
        os.m22 = 0;
        **/
    }

    // Update is called once per frame
    void Update()
    {
        Mesh theMesh = GetComponent<MeshFilter>().mesh;
        Vector2[] uv = theMesh.uv;
        Matrix3x3 t = CreateTRS((Position), Rotation, Scale);
        Matrix3x3 p = Matrix3x3Helpers.CreateTranslation(Position);
        Matrix3x3 r = Matrix3x3Helpers.CreateRotation(Rotation);
        //Matrix3x3 r = ones;//= new Matrix3x3();
        //Matrix3x3 s = ones;//= new Matrix3x3();
        //t = Matrix3x3Helpers.;

        if (anim == true)
        {
            for (int i = 0; i < uv.Length; i++)
            {
                //uv[i].x = initUV[i].x * Scale.x;
                //uv[i].y = initUV[i].y * Scale.y;
                //uv[i] = Position + uv[i];
                uv[i] = p * r * uv[i];
                //Debug.Log(uv[i]);
            }
        }
        else
        {
            for (int i = 0; i < uv.Length; i++)
            {
                //uv[i].x = initUV[i].x * Scale.x;
                //uv[i].y = initUV[i].y * Scale.y;
                //uv[i] = Position + uv[i];
                uv[i] = t * initUV[i];
                //Debug.Log(uv[i]);
            }
        }
        
        
        theMesh.uv = uv;
    }
}
