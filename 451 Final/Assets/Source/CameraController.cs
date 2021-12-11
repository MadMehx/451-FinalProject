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

    void FixedUpdate()
    {
        if (player == null && lookAtPosition == null)
        {
            player = GameManagerASL.playerBoat.GetComponentInChildren<BoatMovement>().gameObject;
            lookAtPosition = player.transform;
        }

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            //transform.up = Vector3.up;
            //transform.forward = (lookAtPosition.transform.localPosition - transform.localPosition).normalized;

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
            var newPos = Vector3.Lerp(transform.position, player.transform.position - (-player.transform.forward) * 10f, Time.deltaTime);
            //newPos.y += 0.1f;
            transform.position = newPos;
            transform.LookAt(player.transform.position);
            transform.position = new Vector3(transform.position.x,
                                             transform.position.y + 0.1f,
                                             transform.position.z);
        }
    }

    private void ComputeOrbit(Quaternion q)
    {
        Matrix4x4 r = Matrix4x4.TRS(Vector3.zero, q, Vector3.one);
        Matrix4x4 invP = Matrix4x4.TRS(-lookAtPosition.localPosition, Quaternion.identity, Vector3.one);
        r = invP.inverse * r * invP;
        Vector3 newCameraPos = r.MultiplyPoint(transform.localPosition);

        if (Mathf.Abs(Vector3.Dot(newCameraPos.normalized, Vector3.up)) < 0.985)
        {
            transform.localPosition = newCameraPos;
            transform.LookAt(lookAtPosition);
        }
    }
}
