using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPV_PlayerMovment : MonoBehaviour
{

    float horizontalInput;
    float verticalInput;

    public float movementSpeed;
    void Start()
    {

    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal"); //declarem el movment
        verticalInput = Input.GetAxis("Vertical"); // declarem el movment

        //movment

        if (Input.GetKey(KeyCode.A))
        {
            transform.position = transform.position + (-transform.right) * movementSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position = transform.position + (transform.right) * movementSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.position = transform.position + (transform.forward) * movementSpeed * Time.deltaTime;
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            transform.position = transform.position + (-transform.forward) * movementSpeed * Time.deltaTime;
        }
    }
}
