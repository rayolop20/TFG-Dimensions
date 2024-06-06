using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{

    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;

    private Vector3 previousPosition;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);
            cam.transform.position = target.position;

            cam.transform.RotateAround(new Vector3(), new Vector3(0, 1, 0), direction.x * 180);
            cam.transform.Translate(new Vector3(0,0,-20));
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        //transform.position = new Vector3(player.position.x, player.position.y + 8.0f, player.position.z + 7.0f);
        //
        //transform.LookAt(player.position);
        //
        //transform.RotateAround(player.transform.position, Vector3.up, Input.GetAxis("Mouse X") * turnSpeed);
    }
}
