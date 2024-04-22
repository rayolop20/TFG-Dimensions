using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempRay : MonoBehaviour
{
    // Start is called before the first frame update
    public class HitObjects
    {
        public int id;
        public Vector3 initPosition;
        public Vector3 endPosition;
    }

    public float rayLength = 10f; // Longitud del rayo


    int hitsCount = 0;
    Vector3 LastHitPosition;
    private List<Vector3> newPositions = new();
    private List<Vector3> lastPositions = new();
    public Dictionary<int, HitObjects> hObjetcs = new();
    public List<HitObjects> removedObjects = new();
    private int ObjectNumberId = 0;


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
                Vector3 roundedPoint = new Vector3((float)Math.Round(hitInfo.point.x, 2),
                                                   (float)Math.Round(hitInfo.point.y, 2),
                                                   (float)Math.Round(hitInfo.point.z, 2));

                newPositions.Add(roundedPoint );

                LastHitPosition = roundedPoint + new Vector3(0, 0, 0.01f); // + 0.01 per no tornar a colisionar
                Debug.DrawRay(LastHitPosition, transform.forward * hitInfo.distance, Color.red);
            }
        }

        //calcular i adegir objectes i posicions noves

        if (newPositions.Count == 0)
        {
            hObjetcs.Clear();
        }
        else
        {
            for (int i = 0; i <= newPositions.Count; i++)
            {
                if (i % 2 == 0 && i != 0)
                {
                    ObjectNumberId = (i / 2) - 1;

                    if (lastPositions.Count < newPositions.Count && !hObjetcs.ContainsKey(ObjectNumberId)) // comparar si tamany es diferent i el objectes esta creat o no
                    {
                        HitObjects colidedObject = new HitObjects();
                        colidedObject.initPosition = newPositions[i - 2];
                        colidedObject.endPosition = newPositions[i - 1];
                        colidedObject.id = ObjectNumberId;
                        hObjetcs.Add(ObjectNumberId, colidedObject);
                    }
                    //else if (lastPositions[i - 2] != newPositions[i - 2] || lastPositions[i - 1] != newPositions[i - 1]) // mirar si les posicions son diferents
                    //{
                    //    actualizePositions(ObjectNumberId, i);
                    //}

                }
            }
            if (lastPositions.Count > newPositions.Count)
            {
                for (int i = 0; i <= lastPositions.Count/2; i++)
                {
                    if (!newPositions.Contains(hObjetcs[i].initPosition))
                    {
                        removedObjects.Add(hObjetcs[i]);
                        hObjetcs.Remove(hObjetcs[i].id);//Revisar
                        lastPositions = new List<Vector3>(newPositions);
                    }

                }
            }


        }
        Debug.Log("Positions: " + newPositions.Count.ToString());
        Debug.Log("Positions: " + hObjetcs.Count.ToString());
        lastPositions = new List<Vector3>(newPositions);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * rayLength);
    }

    private void actualizePositions(int objectId, int listNum)
    {
        hObjetcs[objectId].initPosition = newPositions[listNum - 2];
        hObjetcs[objectId].endPosition = newPositions[listNum - 1];
    }
}
