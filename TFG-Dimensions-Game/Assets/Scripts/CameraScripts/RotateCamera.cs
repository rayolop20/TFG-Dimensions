using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public Transform player; // Referencia al transform del jugador
    public float distance = 5.0f; // Distancia desde el jugador
    public float height = 3.0f; // Altura de la cámara
    public float rotationSpeed = 100.0f; // Velocidad de rotación

    private float currentRotationAngle = 0.0f; // Ángulo de rotación actual alrededor del jugador
    private Vector3 offset; // Offset inicial de la cámara

    void Start()
    {
        // Calcula el offset inicial basado en la distancia y la altura
        offset = new Vector3(0, height, -distance);
    }

    void LateUpdate()
    {
        // Manejar la entrada del ratón para la rotación de la cámara
        if (Input.GetMouseButton(1)) // Si se mantiene presionado el botón derecho del ratón
        {
            float horizontalInput = Input.GetAxis("Mouse X");
            currentRotationAngle += horizontalInput * rotationSpeed * Time.deltaTime;
        }

        // Calcular la posición deseada de la cámara
        Quaternion rotation = Quaternion.Euler(45, currentRotationAngle, 0); // 45 grados hacia abajo y rotación alrededor del jugador
        Vector3 desiredPosition = player.position + rotation * offset;

        // Establecer la posición de la cámara
        transform.position = desiredPosition;

        // Hacer que la cámara mire al jugador
        transform.LookAt(player.position + Vector3.up * (height / 2));
    }
}
