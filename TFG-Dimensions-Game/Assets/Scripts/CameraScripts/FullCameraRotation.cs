using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullCameraRotation : MonoBehaviour
{

    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.RotateAround(Vector3.zero, Vector3.up, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.RotateAround(Vector3.zero, Vector3.up, -rotationSpeed * Time.deltaTime);

        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.RotateAround(Vector3.zero, Vector3.left, -rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.RotateAround(Vector3.zero, Vector3.left, rotationSpeed * Time.deltaTime);
        }
    }
}
