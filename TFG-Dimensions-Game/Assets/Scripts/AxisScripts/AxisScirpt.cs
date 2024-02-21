using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisScirpt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.RotateAround(Vector3.zero, Vector3.up, 90);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.RotateAround(Vector3.zero, Vector3.up, -90);

        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.RotateAround(Vector3.zero, Vector3.left, -90);
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.RotateAround(Vector3.zero, Vector3.left, 90);
        }
    }
}
