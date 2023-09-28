using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public float moveSpeed;
    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirections;

    Rigidbody player;
    void Start()
    {
        player = GetComponent<Rigidbody>();
        player.freezeRotation = true;
    }

    void Update()
    {
        MyInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horinzontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        // calculate movment direction
        moveDirections = orientation.forward * verticalInput + orientation.right * horizontalInput;
        player.AddForce(moveDirections.normalized * moveSpeed * 10f, ForceMode.Force);
    }
}
