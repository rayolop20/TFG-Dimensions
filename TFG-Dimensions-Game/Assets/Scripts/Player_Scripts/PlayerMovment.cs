using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{

    float horizontalInput;
    float verticalInput;
    public float rotationSpeed;
    public float movementSpeed;
    void Start()
    {

    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal"); //declarem el movment
        verticalInput = Input.GetAxis("Vertical"); // declarem el movment

        //movment
        Vector3 movementDirection = new Vector3(horizontalInput, 0, 0);

        transform.position = transform.position + movementDirection * movementSpeed * Time.deltaTime;

        //rotation
        Vector3 rotationMovment = new Vector3(verticalInput, 0, -verticalInput);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotationMovment), rotationSpeed * Time.deltaTime);

        //if (movementDirection != Vector3.zero)transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(movementDirection), rotationSpeed * Time.deltaTime); 
    }

}