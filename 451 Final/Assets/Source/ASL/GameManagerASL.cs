using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerASL : MonoBehaviour
{
    /// <summary>
    /// Static ref to local player boat object, accessible to other classes
    /// without direct ref.
    /// </summary>
    public static GameObject playerBoat = null;

    [SerializeField]
    private string playerPrefabName;

    void Start()
    {
        ASL.ASLHelper.InstantiateASLObject(playerPrefabName,
            new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), Random.Range(-3, 3)),
            Quaternion.identity,
            string.Empty,
            string.Empty,
            OnPlayerCreated,
            ClaimRejected,
            OnPlayerReceiveFloats);
    }

    void FixedUpdate()
    {
        if (playerBoat == null) return;

        playerBoat.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
        {
            // This could be moved to a delegate that is called when the player
            // moves in order to save bandwidth
            Movement();
        });
    }

    private static void OnPlayerCreated(GameObject _myGameObject)
    {
        playerBoat = _myGameObject;
    }

    public static void ClaimRejected(string _id, int _cancelledCallbacks)
    {
        // Do nothing
    }

    // Multiply player id by multiples of 10 to separate different input
    // types like movement and color
    private static void OnPlayerReceiveFloats(string _id, float[] _floats)
    {
        //var dir = new Vector3(_floats[0], _floats[1], _floats[2]);
        ASL.ASLObject networkedPlayerBoat;
        ASL.ASLHelper.m_ASLObjects.TryGetValue(_id, out networkedPlayerBoat);
        networkedPlayerBoat.GetComponent<Rigidbody>().velocity = new Vector3(2, 0);
    }

    private void Movement()
    {
        float[] moveDir = new float[] {
            //vector
        };
        playerBoat.GetComponent<ASL.ASLObject>().SendFloatArray(moveDir);
    }
}
