using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//public class HitObjects
//{
//    public Vector3 initPosition;
//    public Vector3 endPosition;
//}

public class RayLogic : MonoBehaviour
{
    // Start is called before the first frame update


    public float rayLength = 10f; // Longitud del rayo


    int hitsCount = 0;
    Vector3 LastHitPosition;
    private List<Vector3> newPositions = new();
    private List<Vector3> lastPositions = new();
    public Dictionary<int, HitObjects> hObjetcs = new();
    public List<int> removedObjects = new();
    private int ObjectNumberId;


    private void Start()
    {
        ObjectNumberId = 0;
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

                newPositions.Add(roundedPoint);

                LastHitPosition = roundedPoint + new Vector3(0, 0, 0.01f); // + 0.01 per no tornar a colisionar
                Debug.DrawRay(LastHitPosition, transform.forward * hitInfo.distance, Color.red);
            }
        }


        //calcular i adegir objectes i posicions noves

        if (newPositions.Count % 2 != 1 && lastPositions.Count % 2 != 1)
        {

            int vuelta_num = 0;
            foreach (KeyValuePair<int, HitObjects> planeValue in hObjetcs)
            {
                int _key = planeValue.Key;

                if (lastPositions.Count > newPositions.Count && !newPositions.Contains(planeValue.Value.initPosition))
                {

                    removedObjects.Add(_key);
                    hObjetcs.Remove(_key);
                    lastPositions = new List<Vector3>(newPositions);

                    Debug.Log("entro");
                    return;

                }
                else if (lastPositions.Count == newPositions.Count && (!newPositions.Contains(planeValue.Value.initPosition) || !newPositions.Contains(planeValue.Value.endPosition)) && (newPositions.Count / 2) == hObjetcs.Count)
                {

                    hObjetcs[_key].initPosition = newPositions[vuelta_num];
                    hObjetcs[_key].endPosition = newPositions[vuelta_num + 1];
                    // actualizePositions(_key, vuelta_num);                                                                    //Revisar
                }                                                                                                                      //Revisar
                vuelta_num = vuelta_num + 2;                                                                                           //Revisar
            }

            for (int i = 0; i < newPositions.Count; i += 2)
            {
                if (lastPositions.Count < newPositions.Count && !lastPositions.Contains(newPositions[i]) && !hObjetcs.ContainsKey(ObjectNumberId) && i % 2 == 0) // comparar si tamany es diferent i el objectes esta creat o no 
                {
                    HitObjects colidedObject = new HitObjects();
                    colidedObject.initPosition = newPositions[i];
                    colidedObject.endPosition = newPositions[i + 1];
                    hObjetcs.Add(ObjectNumberId, colidedObject);
                    lastPositions.Add(newPositions[i]);
                    lastPositions.Add(newPositions[i + 1]);
                    ObjectNumberId++;
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

    private void actualizePositions(int objectId, int listNum)
    {
        hObjetcs[objectId].initPosition = newPositions[listNum];
        hObjetcs[objectId].endPosition = newPositions[listNum + 1];
    }
}
