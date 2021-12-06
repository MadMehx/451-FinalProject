using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneNode : MonoBehaviour {

    protected Matrix4x4 mCombinedParentXform;
    
    public Vector3 NodeOrigin = Vector3.zero;
    public bool Wobbles = false;
    private bool leftMoving;
    public List<NodePrimitive> PrimitiveList;

	// Use this for initialization
	protected void Start () {
        InitializeSceneNode();
	}
	
	// Update is called once per frame
	void Update () {
        if (Wobbles){
            Vector3 rot = transform.localEulerAngles;
            float wobbler = rot.y;
            if (leftMoving)
                wobbler += .1f;
            else wobbler -= .1f;
            if (wobbler > 270 && leftMoving)
                leftMoving = false;
            if (wobbler < 90 && !leftMoving)
                leftMoving = true;
            transform.localRotation = Quaternion.Euler(rot.x,wobbler,rot.z);
        }
	}
    public Matrix4x4 GetMatrix(){
        return mCombinedParentXform;
    }

    private void InitializeSceneNode()
    {
        mCombinedParentXform = Matrix4x4.identity;
    }

    // This must be called _BEFORE_ each draw!! 
    public void CompositeXform(ref Matrix4x4 parentXform)
    {
        Matrix4x4 orgT = Matrix4x4.Translate(NodeOrigin);
        Matrix4x4 trs = Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale);
        
        mCombinedParentXform = parentXform * orgT * trs;

        // propagate to all children
        foreach (Transform child in transform)
        {
            SceneNode cn = child.GetComponent<SceneNode>();
            if (cn != null)
            {
                cn.CompositeXform(ref mCombinedParentXform);
            }
        }
        
        // disenminate to primitives
        foreach (NodePrimitive p in PrimitiveList)
        {
            p.LoadShaderMatrix(ref mCombinedParentXform);
        }

    }
}