/*
 * Author: Bill Pham
 */

using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform lookAtPosition;
    [SerializeField]
    private Vector3 offset;

    private float prevMouseX = 0f;
    private float prevMouseY = 0f;

    private GameObject player = null;

    void Update()
    {
        if (player == null && lookAtPosition == null)
        {
            player = GameManagerASL.playerBoat.GetComponentInChildren<BoatMovement>().gameObject;
            lookAtPosition = player.transform;
        }

        // LookAt() without rotation
        transform.up = Vector3.up;
        transform.forward = (lookAtPosition.transform.localPosition - transform.localPosition).normalized;

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            // On first press
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                // Get start pos
                prevMouseX = Input.mousePosition.x;
                prevMouseY = Input.mousePosition.y;
            } 
            // On holding
            else if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
            {
                // Get distance between current and start to calculate move
                // distance
                var distanceX = prevMouseX - Input.mousePosition.x;
                var distanceY = prevMouseY - Input.mousePosition.y;

                if (Input.GetMouseButton(0)) // Tumble
                {
                    // Shrink distance so camera doesn't go flying
                    distanceX *= 0.1f;
                    distanceY *= 0.1f;

                    Quaternion sideways = Quaternion.AngleAxis(-distanceX, transform.up);
                    ComputeOrbit(sideways);

                    Quaternion upDown = Quaternion.AngleAxis(distanceY, transform.right);
                    ComputeOrbit(upDown);
                }
                else if (Input.GetMouseButton(1)) // Track
                {
                    //// Shrink distance so camera doesn't go flying
                    //distanceX *= 0.01f;
                    //distanceY *= 0.01f;

                    //Vector3 panDistance = distanceX * transform.right + distanceY * transform.up;
                    //transform.localPosition += panDistance;
                    //lookAtPosition.localPosition += panDistance;
                }

                // Get new start pos
                prevMouseX = Input.mousePosition.x;
                prevMouseY = Input.mousePosition.y;
            }

            // Dolly
            var scroll = -Input.GetAxis("Mouse ScrollWheel");
            Vector3 dir = transform.localPosition - lookAtPosition.localPosition;
            transform.localPosition += scroll * dir;
        } 
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, lookAtPosition.localPosition + offset, 1f * Time.deltaTime);
        }
    }

    private void ComputeOrbit(Quaternion q)
    {
        Matrix4x4 r = Matrix4x4.Rotate(q);
        Matrix4x4 invP = Matrix4x4.TRS(-lookAtPosition.localPosition, Quaternion.identity, Vector3.one);
        r = invP.inverse * r * invP;
        Vector3 newCameraPos = r.MultiplyPoint(transform.localPosition);

        var v = lookAtPosition.position;
        v.y = newCameraPos.y;
        if (Vector3.Angle(newCameraPos, v) >= 10)
        {
            transform.localPosition = newCameraPos;
            transform.LookAt(lookAtPosition);
        }
    }
}
