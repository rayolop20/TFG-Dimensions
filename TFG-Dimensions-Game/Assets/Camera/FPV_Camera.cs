using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPV_Camera : MonoBehaviour
{
    // Start is called before the first frame update
    public float sensX;
    public float sensY;

    public Transform orientation;

    private float xRotation;
    private float yRotation;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //cursor block y hide
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //obtenir posicion ratoli
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
        
        
        xRotation += mouseY * -1;
        yRotation -= mouseX * -1;

        //modificar rotació camera (cursor), rotació de la camera segons cursor, orientacio segons l'objecte
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        
        orientation.rotation = Quaternion.Euler(0, yRotation,0);
    }
}
