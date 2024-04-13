using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealRender2D : MonoBehaviour
{
    public class HitObjects
    {
        public Vector3 initPosition;
        public Vector3 endPosition;
    }

    public float rayLength = 10f; // Longitud del rayo
    public LayerMask layerMask; // Máscara de capas para la detección de colisiones


    int hitsCount = 0;
    Vector3 LastHitPosition;
    private List<Vector3> newPositions = new();
    private List<Vector3> lastPositions = new();
    public List<HitObjects> hObjetcs = new();


    private void Start()
    {
        // Comprobar si el rayo colisiona con algo
        Physics.queriesHitBackfaces = true;
    }
    void Update()
    {
        newPositions.Clear();
        hitsCount = 0;
        LastHitPosition = transform.position;

        //calcular numero de hits del rayo
        for (int i = 0; i <= hitsCount; i++)
        {

            Ray ray = new Ray(LastHitPosition, transform.forward);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, rayLength))
            {
                hitsCount++; // suma quan fa hit
                newPositions.Add(hitInfo.point);
                LastHitPosition = hitInfo.point + new Vector3(0, 0, 0.01f); // + 0.01 per no tornar a colisionar

                // Dubiaxar raig a l'escena
                Debug.DrawRay(LastHitPosition, transform.forward * hitInfo.distance, Color.red);
            }
        }

        //calcular i adegir objectes i posicions noves

        for (int i = 0; i <= newPositions.Count; i++)
        {
            if (i % 2 == 0 && i != 0)
            {
                if (lastPositions.Count != newPositions.Count) // comparar si tamany es diferent
                {
                    HitObjects colidedObject = new HitObjects();
                    colidedObject.initPosition = newPositions[i - 2];
                    colidedObject.endPosition = newPositions[i - 1];
                    hObjetcs.Add(colidedObject);
                    Debug.Log("LUPACO CABRON: ");
                }
                else if (lastPositions[i - 2] != newPositions[i - 2] || lastPositions[i - 1] != newPositions[i - 1]) // mirar si les posicions son diferents
                {

                    int  ObjectNumberId = (i / 2) - 1;
                    hObjetcs[ObjectNumberId].initPosition = newPositions[i - 2];
                    hObjetcs[ObjectNumberId].endPosition = newPositions[i - 1]; // revisar
                }

            }
        }
        lastPositions = new List<Vector3>(newPositions);
    } 

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * rayLength);
    }

}
