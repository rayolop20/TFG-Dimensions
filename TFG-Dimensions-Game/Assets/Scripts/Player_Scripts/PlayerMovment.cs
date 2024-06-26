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

    public bool playerIsOnGround = true;

    [HideInInspector] public int coinNumber = 0;

    public GameObject options;

    CameraSwitch switchCamera;

    Rigidbody rb;
    void Start()
    {
        switchCamera = gameObject.GetComponent<CameraSwitch>();
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal"); //declarem el movment
        verticalInput = Input.GetAxis("Vertical"); // declarem el movment

        //movment
        Vector3 movementDirection = new Vector3(horizontalInput, 0, 0);
        Vector3 rotationMovment = new Vector3(0,verticalInput * rotationSpeed * Time.deltaTime, 0);

        if (Input.GetKey(KeyCode.D) && switchCamera.cameraActive == false)
        {
            transform.position = transform.position + transform.right * movementSpeed * Time.deltaTime;
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            if (playerIsOnGround == true)
            {
                gameObject.GetComponent<Animator>().SetBool("Walk", true);
            }
        }
        else if (Input.GetKey(KeyCode.A) && switchCamera.cameraActive == false)
        {
            transform.position = transform.position + (-transform.right) * movementSpeed * Time.deltaTime;
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            if (playerIsOnGround == true)
            {
                gameObject.GetComponent<Animator>().SetBool("Walk", true);
            }
            
        }
        else if (Input.GetKey(KeyCode.W) && switchCamera.cameraActive == false || Input.GetKey(KeyCode.S) && switchCamera.cameraActive == false)
        {
            transform.Rotate(rotationMovment * -1);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("Walk", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && playerIsOnGround == true && switchCamera.cameraActive == false)
        {
            rb.AddForce(new Vector3(0, JumpPower, 0), ForceMode.Impulse);
            playerIsOnGround = false;
            gameObject.GetComponent<Animator>().SetBool("Jump", true);
        }
        
        if (Input.GetKeyDown(KeyCode.Escape) && playerIsOnGround == true && switchCamera.cameraActive == false)
        {
            options.SetActive(true);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verifica si el jugador ha colisionado con otro objeto
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Platform")
        {
            playerIsOnGround = true;
            gameObject.GetComponent<Animator>().SetBool("Jump", false);
        }


    }
}