using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public Transform player; // Referencia al transform del jugador
    public float distance = 5.0f; // Distancia desde el jugador
    public float height = 3.0f; // Altura de la c�mara
    public float rotationSpeed = 100.0f; // Velocidad de rotaci�n

    private float currentRotationAngle = 0.0f; // �ngulo de rotaci�n actual alrededor del jugador
    private Vector3 offset; // Offset inicial de la c�mara

    void Start()
    {
        // Calcula el offset inicial basado en la distancia y la altura
        offset = new Vector3(0, height, -distance);
    }

    void LateUpdate()
    {
        // Manejar la entrada del rat�n para la rotaci�n de la c�mara
        if (Input.GetMouseButton(1)) // Si se mantiene presionado el bot�n derecho del rat�n
        {
            float horizontalInput = Input.GetAxis("Mouse X");
            currentRotationAngle += horizontalInput * rotationSpeed * Time.deltaTime;
        }

        // Calcular la posici�n deseada de la c�mara
        Quaternion rotation = Quaternion.Euler(45, currentRotationAngle, 0); // 45 grados hacia abajo y rotaci�n alrededor del jugador
        Vector3 desiredPosition = player.position + rotation * offset;

        // Establecer la posici�n de la c�mara
        transform.position = desiredPosition;

        // Hacer que la c�mara mire al jugador
        transform.LookAt(player.position + Vector3.up * (height / 2));
    }
}
