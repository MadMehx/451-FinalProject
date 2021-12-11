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
            new Vector3(0, 0, 0),
            Quaternion.identity,
            string.Empty,
            string.Empty,
            OnPlayerCreated,
            ClaimRejected,
            OnPlayerReceiveFloats);
    }

    void Update()
    {
        if (playerBoat == null) return;

        playerBoat.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
        {

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
        ASL.ASLObject networkedPlayerBoat;
        ASL.ASLHelper.m_ASLObjects.TryGetValue(_id, out networkedPlayerBoat);

        // Color
        // Rotation
        if (_floats[3] >= 10f)
        {
            var rotation = Quaternion.Euler(_floats[0], _floats[1], _floats[2]);
            networkedPlayerBoat.transform.GetChild(0).transform.rotation = rotation;
        }
        // Movement
        else
        {
            var direction = new Vector3(_floats[0], _floats[1], _floats[2]);
            networkedPlayerBoat.transform.GetChild(0).GetComponent<Rigidbody>().AddForce(direction);
        }
    }
}
