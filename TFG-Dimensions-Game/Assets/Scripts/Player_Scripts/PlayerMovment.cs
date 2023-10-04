using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{

    float horizontalInput;
    float verticalInput;
    public float rotationSpeed;
    public float movementSpeed;
    public float JumpPower;
    void Start()
    {

    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal"); //declarem el movment
        verticalInput = Input.GetAxis("Vertical"); // declarem el movment

        //movment
        Vector3 movementDirection = new Vector3(horizontalInput, 0, 0);
        Vector3 rotationMovment = new Vector3(0,verticalInput * rotationSpeed * Time.deltaTime, 0);

        if (Input.GetKey(KeyCode.A))
        {
            transform.position = transform.position + transform.right * movementSpeed * Time.deltaTime;
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = transform.position + (-transform.right) * movementSpeed * Time.deltaTime;
        }
     
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            transform.Rotate(rotationMovment);
        }

        if (Input.GetKey(KeyCode.Space))
        {

        }
    }

}