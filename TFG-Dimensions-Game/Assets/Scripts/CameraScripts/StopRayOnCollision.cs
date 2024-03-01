using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StopRayOnCollision : MonoBehaviour
{
    public float rayLength = 10f; // Longitud del rayo
    public LayerMask layerMask; // Máscara de capas para la detección de colisiones
    public GameObject plane;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hitInfo;

        // Comprobar si el rayo colisiona con algo
        if (Physics.Raycast(ray, out hitInfo, rayLength, layerMask))
        {
            Debug.Log("Objeto colisionado: " + hitInfo.collider.gameObject.name);

             Vector3 info = hitInfo.point;
            Vector3 exit = ray.GetPoint(rayLength);

            Vector3 scale = hitInfo.collider.bounds.size;

            Debug.Log("Escala del objeto: " + exit);
            Debug.Log("Escala del objeto: " + info);

            Debug.DrawRay(transform.position, transform.forward * rayLength, Color.red);
        }
    }
}